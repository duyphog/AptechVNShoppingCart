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
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IUrlHelper urlHelper)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsAsync([FromQuery] ProductParameters productParameters)
        {
            var result = await _productService.FindAll(productParameters);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            var list = result.Value;

            if (list.TotalCount == 0)
                return NoContent();

            Response.AddPagination(list.TotalCount, list.PageSize, list.CurrentPage, list.TotalPages, list.HasPrevious, list.HasNext);

            return Ok(list);
        }

        [HttpGet("{id}", Name = nameof(GetProductByIdAsync))]
        public async Task<ActionResult> GetProductByIdAsync(string id)
        {
            var result = await _productService.FindById(id);
            if (!result.Succeed)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));
            }

            return result.Value != null
                ? Ok(result.Value)
                : NotFound(new ErrorResponse(HttpStatusCode.BadRequest, "Warning", "Not exist value"));
        }

        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProductAsync([FromForm] ProductForCreate model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _productService.CreateProductAsync(model);
            return result.Succeed
                ? CreatedAtRoute( nameof(GetProductByIdAsync), new {id = result.Value.Id}, result.Value)
                : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Create fail", result.Errors));
        }

        [Consumes("multipart/form-data")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateAsync(string id, [FromForm] ProductForUpdate model)
        {
            if (id != model.Id)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Id Invalid"));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }
            //var keyvalue = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString())["photos"].ToString();
            //model.Photos = JsonConvert.DeserializeObject<ProductPhotoDTO[]>(keyvalue);

            var result = await _productService.UpdateProductAsync(model);
            return result.Succeed
                ? Ok(result.Value)
                : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));
        }

        [HttpDelete("{id}", Name = nameof(DeleteAsync))]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await _productService.DeleteProductAsync(id);

            return result.Succeed ? NoContent() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));
        }
    }
}
