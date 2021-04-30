using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Extentions;
using Api.Models;
using Contracts;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductPhotoController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUrlHelper _urlHelper;

        public ProductPhotoController(IProductService productService, IUrlHelper urlHelper)
        {
            _productService = productService;
            _urlHelper = urlHelper;
        }

        [AllowAnonymous]
        [HttpPost("photo/{id}", Name = nameof(UploadProductPhotosAsync))]
        public async Task<ActionResult<IEnumerable<ProductPhotoDTO>>> UploadProductPhotosAsync(string id, IFormFile[] files)
        {
            if (files.Length == 0)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Upload fail", "No select file"));
            }

            var result = await _productService.UploadPhotosAsync(id, files);

            return result.Succeed
                ? Ok(new
                {
                    value = result.Value.Select(x => ExpandSingleItem(x)),
                    links = CreateLinksForCollection()
                })
                : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Upload fail", result.Errors));
        }

        [HttpDelete("{id}", Name = nameof(DeleteProductPhotosAsync))]
        public async Task<ActionResult<IEnumerable<ProductPhotoDTO>>> DeleteProductPhotosAsync(Guid id)
        {
            var result = await _productService.DeleteProductPhotoAsync(id);

            return result.Succeed ? NoContent() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));
        }

        private List<Link> CreateLinksForCollection()
        {
            var links = new List<Link>
            {
                new Link(_urlHelper.Link(nameof(UploadProductPhotosAsync), null), "create", "POST")
            };
            return links;
        }

        private dynamic ExpandSingleItem(ProductPhotoDTO photo)
        {
            var links = GetLinks(photo.Id);

            var resourceToReturn = photo.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<Link> GetLinks(Guid id)
        {
            var links = new List<Link>
            {
            //     new Link(_urlHelper.Link(nameof(GetProductByIdAsync), new { id }),
            //   "self",
            //   "GET"),

                new Link(_urlHelper.Link(nameof(UploadProductPhotosAsync), null),
              "create",
              "POST"),

                new Link(_urlHelper.Link(nameof(DeleteProductPhotosAsync), new { id }),
              "delete",
              "DELETE")
            };

            return links;
        }
    }
}
