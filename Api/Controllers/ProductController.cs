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

            return result.Value != null ? Ok(result.Value) : NotFound(new ErrorResponse(HttpStatusCode.BadRequest, "Warning", "Not exist value"));
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
    }
}
