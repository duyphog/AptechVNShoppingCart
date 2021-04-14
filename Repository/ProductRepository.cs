using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(Entities.ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Product>> GetAllProductAsync(ProductParameters productParameters)
        {
            var queries = FindAll().OrderBy(p => p.Id).AsQueryable();

            return await PagedList<Product>.ToPagedList(queries, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            //return await FindByCondition(1==1).FirstOrDefaultAsync();
            return null;
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
