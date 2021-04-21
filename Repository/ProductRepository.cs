using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Helpers;
using Entities.Models;
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
    }
}
