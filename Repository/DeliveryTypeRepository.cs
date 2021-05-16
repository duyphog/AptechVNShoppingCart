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
    public class DeliveryTypeRepository : RepositoryBase<DeliveryType>, IDeliveryTypeRepository
    {
        public DeliveryTypeRepository(ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<DeliveryType>> FindAllDeliveryTypeAsync()
        {
            return await FindAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<DeliveryType> FindDeliveryByIdAsync(string id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
