using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.Data.SqlClient;
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
            return await FindByCondition(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public int GetNewProductCodeFromSequence()
        {
            var param = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            AppContext.Database.ExecuteSqlRaw("set @result = next value for productCode_seq", param);
            return (int)param.Value;
        }

    }
}
