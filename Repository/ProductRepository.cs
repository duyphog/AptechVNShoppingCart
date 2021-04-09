using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public PagedList<Product> GetAllProductAsync(ProductParameters productParameters)
        {
            var queries = FindAll().OrderBy(p => p.Id);

            return PagedList<Product>.ToPagedList(queries, productParameters.PageNumber, productParameters.PageSize);
            
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await FindByCondition(p => p.Id == id).FirstOrDefaultAsync();
        }


        public void CreateProduct(Product product)
        {
            Create(product);
        }

        public void DeleteProduct(Product product)
        {
            Update(product);
        }

        public void UpdateProduct(Product product)
        {
            Delete(product);
        }
    }
}
