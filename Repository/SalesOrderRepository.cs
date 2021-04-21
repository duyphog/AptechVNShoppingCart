using System;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class SalesOrderRepository : RepositoryBase<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(ShoppingCartContext context) : base(context)
        {
        }
    }
}
