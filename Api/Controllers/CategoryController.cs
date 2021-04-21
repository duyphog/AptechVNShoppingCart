using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.AppServices;
using Api.Models;
using Contracts;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var result = await _categoryService.FindAllAsync();

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetById(string id)
        {
            var result = await _categoryService.FindByIdAsync(id);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));

            if (result.Value == null)
                return NoContent();

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CategoryForCreate model)
        {
            var result = await _categoryService.CreateCategoryAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Create fail", result.Errors));

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> Update(string id, CategoryForUpdate model)
        {
            if(id != model.Id)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Invalid Id"));
            }
            var result = await _categoryService.UpdateCategoryAsync(model);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(string id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            if (result.Succeed == false)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));

            return NoContent();
        }
    }
}
