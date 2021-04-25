using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Api.AppServices
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryService(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper,
            IMapper mapper) : base(httpContextAccessor, repoWrapper)
        {
            _mapper = mapper;
        }

        
        public async Task<ProcessResult<IEnumerable<CategoryDTO>>> FindAllAsync()
        {
            async Task<IEnumerable<CategoryDTO>> acction()
            {
                var catgs = await _repoWrapper.Category.FindAllAsync();
                return _mapper.Map<IEnumerable<CategoryDTO>>(catgs);
            }

            return await Process.RunAsync(acction);
        }


        public async Task<ProcessResult<CategoryDTO>> FindByIdAsync(string id)
        {
            async Task<CategoryDTO> acction()
            {
                var catg = await _repoWrapper.Category.FindById(id);
                return _mapper.Map<CategoryDTO>(catg);
            }

            return await Process.RunAsync(acction);
        }

        public async Task<ProcessResult<CategoryDTO>> CreateCategoryAsync(CategoryForCreate model)
        {
            async Task<CategoryDTO> acction()
            {
                var entity = await _repoWrapper.Category.FindByName(model.Name);
                if (entity != null)
                    throw new InvalidOperationException($"Name is exist, category Name: {entity.Name}");


                var cartg = _mapper.Map<Category>(model);
                
                cartg.Status = true;
                cartg.CreateBy = CurrentUser.UserName;
                cartg.CreateDate = DateTime.UtcNow;

                _repoWrapper.Category.CreateCategory(cartg);
                return await _repoWrapper.SaveAsync() <= 0 ? throw new Exception("Save fail") : _mapper.Map<CategoryDTO>(cartg);
            }

            return await Process.RunAsync(acction);
        }

 
        public async Task<ProcessResult<CategoryDTO>> UpdateCategoryAsync(CategoryForUpdate model)
        {
            async Task<CategoryDTO> acction()
            {
                var category = await _repoWrapper.Category.FindById(model.Id);
                if(category == null)
                {
                    throw new InvalidOperationException("Id is not exist");
                }

                _ = _mapper.Map(model, category);

                category.ModifyBy = CurrentUser.UserName;
                category.ModifyDate = DateTime.UtcNow;

                _repoWrapper.Category.Update(category);
 
                return await _repoWrapper.SaveAsync() <= 0 ? throw new Exception("Save fail") : _mapper.Map<CategoryDTO>(category);
            }

            return await Process.RunAsync(acction);
        }

        public async Task<ProcessResult> DeleteCategoryAsync(string id)
        {
            async Task action()
            {
                var category = await _repoWrapper.Category.FindById(id);
                if (category == null)
                    throw new InvalidOperationException("Id is not exist");
                if(category.Status == false)
                    throw new InvalidOperationException("Current status is false");

                category.Status = false;
                category.ModifyDate = DateTime.UtcNow;
                category.ModifyBy = CurrentUser.UserName;

                _repoWrapper.Category.Update(category);
                if (await _repoWrapper.SaveAsync() <= 0)
                    throw new Exception("Save fail");
            }
            return await Process.RunAsync(action);
        }
    }
}
