using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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
                var entity = await _repoWrapper.Category.FindById(model.Id);
                if (entity != null)
                    throw new InvalidOperationException($"Id is exist, category Name: {entity.Name}");

                var cartg = _mapper.Map<Category>(model);
                _repoWrapper.Category.Create(cartg);
                var rows = await _repoWrapper.SaveAsync();
                if (rows <= 0)
                    throw new Exception("Save fail");

                return _mapper.Map<CategoryDTO>(cartg);
            }

            return await Process.RunAsync(acction);
        }

 
        public async Task<ProcessResult<CategoryDTO>> UpdateCategoryAsync(CategoryForUpdate model)
        {
            async Task<CategoryDTO> acction()
            {
                var entity = await _repoWrapper.Category.FindById(model.Id);
                if(entity == null)
                {
                    throw new InvalidOperationException("Id is not exist");
                }

                var category = _mapper.Map(model, entity);
                category.ModifyBy = CurrentUser.UserName;
                category.ModifyDate = DateTime.Now;

                _repoWrapper.Category.Update(category);
                var rows = await _repoWrapper.SaveAsync();
                if (rows <= 0)
                    throw new Exception("Save fail");

                return _mapper.Map<CategoryDTO>(entity);
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

                category.Status = false;
                category.ModifyDate = DateTime.Now;
                category.ModifyBy = CurrentUser.UserName;

                var rows = await _repoWrapper.SaveAsync();
                if (rows <= 0)
                    throw new Exception("Save fail");
            }
            return await Process.RunAsync(action);
        }
    }
}
