using System;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Contracts
{
    public interface ICloudDinaryService
    {
        Task<ImageUploadResult> AddAsync(IFormFile file);

        Task<DeletionResult> DeleteAsync(string publicId);
    }
}
