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
    public class ProductPhotoRepository : RepositoryBase<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(ShoppingCartContext context) : base(context)
        {
        }

        public async Task AddRangePhotosAsync(List<ProductPhoto> photos)
        {
            await AddRangeAsync(photos);
        }

        public async Task<ProductPhoto> FindById(Guid id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
