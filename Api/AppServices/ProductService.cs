using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Api.AppServices
{
    public class ProductService : ServiceBase, IProductService
    {
        private readonly ICloudDinaryService _cloudDinaryService;
        private readonly IMapper _mapper;

        public ProductService(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper,
            ICloudDinaryService cloudDinaryService, IMapper mapper)
            : base(httpContextAccessor, repoWrapper)
        {
            _cloudDinaryService = cloudDinaryService;
            _mapper = mapper;
        }

        public async Task<ProcessResult<PagedList<ProductDTO>>> FindAll(ProductParameters parameters)
        {
            async Task<PagedList<ProductDTO>> action()
            {
                var products = await _repoWrapper.Product.FindAllProduct(parameters);

                if (products.CurrentPages > products.TotalPages)
                    throw new Exception("CurrentPages > TotalPages");

                return new PagedList<ProductDTO>(_mapper.Map<List<Product>, List<ProductDTO>>(products),
                   products.TotalCount, products.CurrentPages, products.PageSize);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<ProductDTO>> FindById(string id)
        {
            async Task<ProductDTO> action()
            {
                var product = await _repoWrapper.Product.FindProductByIdAsync(id);
                return _mapper.Map<ProductDTO>(product);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<ProductDTO>> CreateProductAsync(ProductForCreate model)
        {
            async Task<ProductDTO> action()
            {
                if(await _repoWrapper.Category.FindById(model.CategoryId) == null)
                {
                    throw new InvalidOperationException("CategoryId is not exist");
                }

                var entity = await _repoWrapper.Product.FindProductByNameAsync(model.ProductName);
                if (entity != null)
                {
                    throw new InvalidOperationException("ProductName is exist");
                }

                var product = _mapper.Map<Product>(model);
                product.CreateBy = CurrentUser.UserName;
                product.CreateDate = DateTime.Now;
                product.Status = true;

                _repoWrapper.Product.CreateProduct(product);
                return await _repoWrapper.SaveAsync() > 0 ? _mapper.Map<ProductDTO>(product) : throw new InvalidCastException("Save fail");
        }
            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult> DeleteProductAsync(string id)
        {
            async Task action()
            {
                var product = _repoWrapper.Product.FindByCondition(x => x.Id == id).FirstOrDefault();

                if (product == null)
                    throw new InvalidOperationException("Id is not exist");

                product.Status = false;
                product.ModifyDate = DateTime.Now;
                product.ModifyBy = CurrentUser.UserName;

                _repoWrapper.Product.Update(product);

                if (await _repoWrapper.SaveAsync() <= 0)
                    throw new InvalidOperationException("Save Fail");
            }
            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<ProductDTO>> UpdateProductAsync(ProductForUpdate model)
        {
            async Task<ProductDTO> action()
            {
                var product = await _repoWrapper.Product.FindProductByIdAsync(model.Id);
                if (product == null)
                    throw new InvalidOperationException("Id is not exist");

                _ = _mapper.Map(model, product);

                _repoWrapper.Product.Update(product);
                return await _repoWrapper.SaveAsync() > 0 ? _mapper.Map<ProductDTO>(product) : throw new InvalidCastException("Save fail");
            }

            return await Process.RunAsync(action);
        }


        public async Task<ProcessResult<IEnumerable<ProductPhotoDTO>>> UploadPhotosAsync(string id, IFormFile[] files)
        {
            async Task<IEnumerable<ProductPhotoDTO>> action()
            {
                string error = "";

                if(files == null)
                {
                    throw new InvalidOperationException("Select photo upload");
                }

                var product = await _repoWrapper.Product.FindProductByIdAsync(id);
                if (product == null)
                    throw new InvalidOperationException("Id is not exist");

                var photos = new List<ProductPhoto>();

                for (int i = 0; i < files.Length; i++)
                {
                    var resultUpload = await _cloudDinaryService.AddAsync(files[i]);
                    if (resultUpload.Error != null)
                    {
                        error = resultUpload.Error.Message;
                        break;
                    }

                    photos.Add(new ProductPhoto
                    {
                        Id = Guid.NewGuid(),
                        ProductId = id,
                        PublicId = resultUpload.PublicId,
                        Url = resultUpload.SecureUrl.AbsoluteUri,
                        IsMain = false
                    });
                }

                if (error.Length > 0)
                {
                    photos.ForEach(async x => await DeletePhotoFromCloudinary(x.PublicId));
                    throw new InvalidOperationException(error);
                }

                if (product.ProductPhotos.Count() == 0)
                    photos[0].IsMain = true;

                await _repoWrapper.ProductPhoto.AddRangeAsync(photos);

                return await _repoWrapper.SaveAsync() > 0
                    ? _mapper.Map<IEnumerable<ProductPhotoDTO>>(photos.AsEnumerable())
                    : throw new InvalidCastException("Save fail");
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult> DeleteProductPhotoAsync(Guid id)
        {
            async Task action()
            {
                var photo = await _repoWrapper.ProductPhoto.FindById(id);
                if(photo == null)
                    throw new InvalidOperationException("Id is not exist");

                await DeletePhotoFromCloudinary(photo.PublicId);

                _repoWrapper.ProductPhoto.Delete(photo);

                if (await _repoWrapper.SaveAsync() <= 0)
                    throw new InvalidCastException("Save fail");
            }

            return await Process.RunAsync(action);
        }

        private async Task DeletePhotoFromCloudinary(string photoPublicId)
        {
            if (photoPublicId != null)
            {
                var resultDelete = await _cloudDinaryService.DeleteAsync(photoPublicId);
                if (resultDelete.Error != null)
                    throw new InvalidOperationException(resultDelete.Error.Message);
            }
        }
    }
}
