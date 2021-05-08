using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllAsync()
        {
            var result = await _categoryService.FindAllAsync();

            if (!result.Succeed)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));

            return result.Value.Any() ? Ok(result.Value) : NotFound(new ErrorResponse(HttpStatusCode.BadRequest, "Warning", "Not exist value"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetByIdAsync(string id)
        {
            var result = await _categoryService.FindByIdAsync(id);

            if (!result.Succeed)
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Fail", result.Errors));

            return result.Value != null ? Ok(result.Value) : NotFound(new ErrorResponse(HttpStatusCode.BadRequest, "Warning", "Not exist value"));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateAsync(CategoryForCreate model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _categoryService.CreateCategoryAsync(model);

            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Create fail", result.Errors));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> UpdateAsync(string id, CategoryForUpdate model)
        {
            if (id != model.Id)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Invalid Id"));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Validation error", errors));
            }

            var result = await _categoryService.UpdateCategoryAsync(model);

            return result.Succeed ? Ok(result.Value) : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Update fail", result.Errors));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(string id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            return result.Succeed ? NoContent() : BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Delete fail", result.Errors));
        }
    }
}
