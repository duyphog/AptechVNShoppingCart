using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IProductPhotoRepository : IRepositoryBase<ProductPhoto>
    {
        void AddRangePhotos(List<ProductPhoto> photos);
        Task<ProductPhoto> FindById(Guid id);
    }
}
