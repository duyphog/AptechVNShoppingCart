using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Helpers;
using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(Entities.ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await FindByCondition(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.ProductPhotos)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<Product>> GetAllProduct(ProductParameters parameters)
        {
             var queries = FindAll()
                    .Include(p => p.Category)
                    .Include(p => p.ProductPhotos)
                    .AsQueryable();

            //if (!String.IsNullOrEmpty(parameters.Fields))
            //{
            //    queries += queries.
            //}
            queries = queries.OrderBy(p => p.Id);
            return await PagedList<Product>.ToPagedList(queries, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await FindByCondition(p => p.ProductName == name)
                .Include(p => p.Category)
                .Include(p => p.ProductPhotos)
                .FirstOrDefaultAsync();
        }

        public int GetNewProductNumberFromSequence()
        {
            var param = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            AppContext.Database.ExecuteSqlRaw("set @result = next value for productNumber_seq", param);
            return  (int)param.Value;
        }
    }
}
