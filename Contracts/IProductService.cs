using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Contracts
{
    public interface IProductService
    {
        Task<ProcessResult<PagedList<ProductDTO>>> FindAll(ProductParameters productParameters);

        Task<ProcessResult<ProductDTO>> FindById(string id);

        Task<ProcessResult<ProductDTO>> CreateProductAsync(ProductForCreate model);

        Task<ProcessResult<ProductDTO>> UpdateProductAsync(ProductForUpdate model);

        Task<ProcessResult> DeleteProductAsync(string id);

        Task<ProcessResult<IEnumerable<ProductPhotoDTO>>> UploadPhotosAsync(string id, IFormFile[] files);

        Task<ProcessResult> DeleteProductPhotoAsync(Guid id);
    }
}
