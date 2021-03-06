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
    public class OrderStatusRepository : RepositoryBase<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync()
        {
            return await FindAll().OrderBy(x => x.Id).ToListAsync();
        }
    }
}
