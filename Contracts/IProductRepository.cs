using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;

namespace Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<ICollection<Product>> GetProductAsync();
        Task<Product> FindProductByIdAsync(string id);
        Task<Product> FindProductByNameAsync(string name);
        Task<PagedList<Product>> FindAllProduct(ProductParameters parameters);
        void CreateProduct(Product product);
    }
}
