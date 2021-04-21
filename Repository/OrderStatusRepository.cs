using System;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class OrderStatusRepository : RepositoryBase<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
