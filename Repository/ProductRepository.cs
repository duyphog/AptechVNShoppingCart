using System;
using System.Collections.Generic;
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

        public async Task<Product> FindProductByIdAsync(string id)
        {
            return await FindByCondition(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.ProductPhotos)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<Product>> FindAllProduct(ProductParameters parameters)
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

        public async Task<Product> FindProductByNameAsync(string name)
        {
            return await FindByCondition(p => p.ProductName == name)
                .Include(p => p.Category)
                .Include(p => p.ProductPhotos)
                .FirstOrDefaultAsync();
        }

        public void CreateProduct(Product product)
        {
            var productNumber = GetNextValueForSequence("productNumber_seq").ToString();
            var subNumber = new string('0', 5 - productNumber.Length);

            product.Id = product.CategoryId + subNumber + productNumber;
            Create(product);
        }

        public async Task<ICollection<Product>> GetProductAsync()
        {
            return await AppContext.Products.ToListAsync();
        }
    }
}
