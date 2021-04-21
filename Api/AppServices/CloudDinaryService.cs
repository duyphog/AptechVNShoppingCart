using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Contracts;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Api.AppServices
{
    public class CloudDinaryService : ICloudDinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudDinaryService(IOptions<CloudinarySettings> config)
        {
            var cloudinaryAccount = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(cloudinaryAccount);
        }

        public async Task<ImageUploadResult> AddAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(444).Width(444).Crop("fill")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeleteAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
