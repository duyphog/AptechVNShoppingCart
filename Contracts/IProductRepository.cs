using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helper;
using Entities.Models;

namespace Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        //Task<IEnumerable<Product>> GetAllProductAsync(ProductParameters productParameters);

        PagedList<Product> GetAllProductAsync(ProductParameters productParameters);

        Task<Product> GetProductByIdAsync(int id);

        void CreateProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);
    }
}
