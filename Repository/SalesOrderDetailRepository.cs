using System;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class SalesOrderDetailRepository : RepositoryBase<SalesOrderDetail>, ISalesOrderDetailRepository
    {
        public SalesOrderDetailRepository(ShoppingCartContext context) : base(context)
        {
        }
    }
}
