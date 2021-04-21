using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Newtonsoft.Json.Linq;

namespace Contracts
{
    public interface ICategoryService
    {
        Task<ProcessResult<IEnumerable<CategoryDTO>>> FindAllAsync();

        Task<ProcessResult<CategoryDTO>> FindByIdAsync(string id);

        Task<ProcessResult<CategoryDTO>> CreateCategoryAsync(CategoryForCreate model);

        Task<ProcessResult<CategoryDTO>> UpdateCategoryAsync(CategoryForUpdate model);

        Task<ProcessResult> DeleteCategoryAsync(string id);
    }
}
