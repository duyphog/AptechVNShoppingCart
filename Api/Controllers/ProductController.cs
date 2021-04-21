using System;
using System.Collections.Generic;
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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetProductsAsync([FromForm] ProductParameters productParameters)
        {
            var result = await _productService.FindAll(productParameters);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }
       
            var products = result.Value;

            if (products.TotalCount == 0)
                return NoContent();

            if (products.CurrentPages > products.TotalPages)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "CurrentPages > TotalPages"));
            }

            Response.AddPagination(products.TotalCount, products.PageSize, products.CurrentPages,
                                    products.TotalPages, products.HasPrevious, products.HasNext);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(string id)
        {
            var result = await _productService.FindById(id);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            if (result.Value == null)
                return NoContent();

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProductAsync(ProductForCreate model)
        {
            var result = await _productService.CreateProductAsync(model);
            if(!result.Succeed)
               return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Create fail", result.Errors));

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateAsync(string id, ProductForUpdate model)
        {
            if(id != model.Id)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Id Invalid"));
            }

            var result = await _productService.UpdateProductAsync(model);
            if(!result.Succeed)
               return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (!result.Succeed)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));

            return NoContent();
        }

        [HttpPost("photo/{id}")]
        public async Task<ActionResult<IEnumerable<ProductPhotoDTO>>> UploadProductPhotosAsync(string id, IFormFile[] files)
        {
            var result = await _productService.UploadPhotosAsync(id, files);

            if (!result.Succeed)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Upload fail", result.Errors));

            return Ok(result.Value);
        }

        [HttpDelete("photo/{id}")]
        public async Task<ActionResult<IEnumerable<ProductPhotoDTO>>> DeleteProductPhotosAsync(Guid id)
        {
            var result = await _productService.DeleteProductPhotoAsync(id);

            if (!result.Succeed)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));

            return NoContent();
        }
    }
}
