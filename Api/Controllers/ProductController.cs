using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Extentions;
using Api.Models;
using Contracts;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Product>> GetProductsAsync([FromForm] ProductParameters productParameters)
        {
            var products = await _repoWrapper.Product.GetAllProductAsync(productParameters);

            if (products.CurrentPages > products.TotalPages)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "CurrentPages > TotalPages"));
            }
                
            if (products.TotalCount == 0)
                return NoContent();

            Response.AddPagination(products.TotalCount, products.PageSize, products.CurrentPages,
                                   products.TotalPages, products.HasPrevious, products.HasNext);

            return Ok(new AppResponse<IEnumerable<Product>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetDetailAsync([FromForm] ProductParameters productParameters)
        {
            var products = await _repoWrapper.Product.GetAllProductAsync(productParameters);

            if (products.CurrentPages > products.TotalPages)
                return BadRequest("CurrentPages > TotalPages");

            if (products.TotalCount == 0)
                return NoContent();

            Response.AddPagination(products.TotalCount, products.PageSize, products.CurrentPages,
                                   products.TotalPages, products.HasPrevious, products.HasNext);

            return Ok(new AppResponse<IEnumerable<Product>>(products));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateAsync([FromForm] ProductParameters productParameters)
        {
            var products = await _repoWrapper.Product.GetAllProductAsync(productParameters);

            if (products.CurrentPages > products.TotalPages)
                return BadRequest("CurrentPages > TotalPages");

            if (products.TotalCount == 0)
                return NoContent();

            Response.AddPagination(products.TotalCount, products.PageSize, products.CurrentPages,
                                   products.TotalPages, products.HasPrevious, products.HasNext);

            return Ok(new AppResponse<IEnumerable<Product>>(products));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteAsync([FromForm] ProductParameters productParameters)
        {
            var products = await _repoWrapper.Product.GetAllProductAsync(productParameters);

            if (products.CurrentPages > products.TotalPages)
                return BadRequest("CurrentPages > TotalPages");

            if (products.TotalCount == 0)
                return NoContent();

            Response.AddPagination(products.TotalCount, products.PageSize, products.CurrentPages,
                                   products.TotalPages, products.HasPrevious, products.HasNext);

            return Ok(new AppResponse<IEnumerable<Product>>(products));
        }

    }
}
