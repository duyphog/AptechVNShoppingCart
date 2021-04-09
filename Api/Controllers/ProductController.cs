using System;
using System.Threading.Tasks;
using Api.Extentions;
using Contracts;
using Entities.Helper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;

        public ProductController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAllAsync([FromQuery] ProductParameters productParameters)
        {
            var products = await _repoWrapper.Product.GetAllProductAsync(productParameters);

            Response.AddPagination(products.TotalCount, products.PageSize, products.CurrentPages,
                                   products.TotalPages, products.HasPrevious, products.HasNext);

            if (products.CurrentPages > products.TotalPages)
                return BadRequest("CurrentPages > TotalPages");

            if (products.TotalCount == 0)
                return NoContent();

            return Ok(products);
        }
    }
}
