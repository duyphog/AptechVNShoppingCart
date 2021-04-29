using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Extentions;
using Api.Models;
using Contracts;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUrlHelper _urlHelper;

        public ProductController(IProductService productService, IUrlHelper urlHelper)
        {
            _productService = productService;
            _urlHelper = urlHelper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetProductsAsync([FromQuery] ProductParameters productParameters)
        {
            var result = await _productService.FindAll(productParameters);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            var list = result.Value;

            if (list.TotalCount == 0)
                return NoContent();

            Response.AddPagination(list.TotalCount, list.PageSize, list.CurrentPages, list.TotalPages, list.HasPrevious, list.HasNext);

            return Ok(list);
        }

        [AllowAnonymous]
        [HttpGet("link", Name = nameof(GetProductsLinkAsync))]
        public async Task<ActionResult<ProductDTO>> GetProductsLinkAsync([FromQuery] ProductParameters productParameters)
        {
            var result = await _productService.FindAll(productParameters);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            var list = result.Value;

            if (list.TotalCount == 0)
                return NoContent();

            Response.AddPagination(list.TotalCount, list.PageSize, list.CurrentPages, list.TotalPages, list.HasPrevious, list.HasNext);

            var links = CreateLinksForCollection(productParameters, list.TotalPages, list.HasNext, list.HasPrevious);

            var toReturn = list.Select(x => ExpandSingleItem(x));

            return Ok(new
            {
                value = toReturn,
                links
            });
        }

        [HttpGet("{id}", Name = nameof(GetProductByIdAsync))]
        public async Task<ActionResult<Product>> GetProductByIdAsync(string id)
        {
            var result = await _productService.FindById(id);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            return result.Value != null ? Ok(result.Value) : NotFound(new ErrorResponse(HttpStatusCode.BadRequest, "Warning", "Not exist value"));
        }

        [HttpPost(Name = nameof(CreateProductAsync))]
        public async Task<ActionResult<ProductDTO>> CreateProductAsync(ProductForCreate model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _productService.CreateProductAsync(model);
            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Create fail", result.Errors));
        }

        [HttpPut("{id}", Name = nameof(UpdateAsync))]
        public async Task<ActionResult<Product>> UpdateAsync(string id, ProductForUpdate model)
        {
            if(id != model.Id)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Id Invalid"));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _productService.UpdateProductAsync(model);
            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));
        }

        [HttpDelete("{id}", Name = nameof(DeleteAsync))]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await _productService.DeleteProductAsync(id);

            return result.Succeed ? NoContent() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));
        }

        [AllowAnonymous]
        [HttpPost("photo/{id}")]
        public async Task<ActionResult<IEnumerable<ProductPhotoDTO>>> UploadProductPhotosAsync(string id, IFormFile[] files)
        {
            if (files.Length == 0)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Upload fail", "No select file"));
            }

            var result = await _productService.UploadPhotosAsync(id, files);

            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Upload fail", result.Errors));
        }

        [HttpDelete("photo/{id}")]
        public async Task<ActionResult<IEnumerable<ProductPhotoDTO>>> DeleteProductPhotosAsync(Guid id)
        {
            var result = await _productService.DeleteProductPhotoAsync(id);

            return result.Succeed ? NoContent() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));
        }

        private List<Link> CreateLinksForCollection(ProductParameters productParameters, int totalPages, bool hasNext, bool hasPrevious)
        {
            var links = new List<Link>
            {
                new Link(_urlHelper.Link(nameof(CreateProductAsync), null), "create", "POST"),

                new Link(_urlHelper.Link(nameof(GetProductsLinkAsync), new
                {
                    pageSize = productParameters.PageSize,
                    pageNumber = productParameters.PageNumber
                }), "self", "GET"),

                new Link(_urlHelper.Link(nameof(GetProductsLinkAsync), new
                {
                    pageSize = productParameters.PageSize,
                    pageNumber = 1
                }), "first", "GET"),

                new Link(_urlHelper.Link(nameof(GetProductsLinkAsync), new
                {
                    pageSize = productParameters.PageSize,
                    pageNumber = totalPages
                }), "last", "GET")
            };

            if (hasNext)
            {
                links.Add(
                new Link(_urlHelper.Link(nameof(GetProductsLinkAsync), new
                {
                    pageSize = productParameters.PageSize,
                    page = productParameters.PageNumber + 1
                }), "next", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                new Link(_urlHelper.Link(nameof(GetProductsLinkAsync), new
                {
                    pageSize = productParameters.PageSize,
                    page = productParameters.PageNumber - 1
                }), "previous", "GET"));
            }

            return links;
        }

        private dynamic ExpandSingleItem(ProductDTO product)
        {
            var links = GetLinks(product.Id);

            var resourceToReturn = product.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<Link> GetLinks(string id)
        {
            var links = new List<Link>
            {
                new Link(_urlHelper.Link(nameof(GetProductByIdAsync), new { id }),
              "self",
              "GET"),

                new Link(_urlHelper.Link(nameof(DeleteAsync), new { id }),
              "delete",
              "DELETE"),

                new Link(_urlHelper.Link(nameof(CreateProductAsync), null),
              "create",
              "POST"),

                new Link(_urlHelper.Link(nameof(UpdateAsync), new { id }),
               "update",
               "PUT")
            };

            return links;
        }
    }
}
