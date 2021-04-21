using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Category>> FindAllAsync()
        {
            return await FindAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Category> FindById(string id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
