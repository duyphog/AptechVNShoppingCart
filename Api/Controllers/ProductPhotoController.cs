using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Models;
using Contracts;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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

        [Consumes("multipart/form-data")]
        [HttpPost("{id}")]
        public async Task<ActionResult<IEnumerable<ProductPhotoDTO>>> UploadProductPhotosAsync(string id, [FromForm(Name = "files")] IFormFile[] files)
        {
            if (files == null || files.Length == 0)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Upload fail", "No select file"));
            }

            var result = await _productService.UploadPhotosAsync(id, files);

            return result.Succeed
                ? Ok(result.Value)
                : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Upload fail", result.Errors));
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductPhotoDTO>>> UpdateProductPhotosIsMainAsync(JObject model)
        {
            
            var result = await _productService.SetMainProductPhotoAsync(model);

            return result.Succeed
                ? Ok(result.Value)
                : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Upload fail", result.Errors));
        }

        [HttpDelete("{id}", Name = nameof(DeleteProductPhotosAsync))]
        public async Task<ActionResult<IEnumerable<ProductPhotoDTO>>> DeleteProductPhotosAsync(Guid id)
        {
            var result = await _productService.DeleteProductPhotoAsync(id);

            return result.Succeed ? NoContent() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));
        }
    }
}
