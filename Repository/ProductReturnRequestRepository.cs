using System;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ProductReturnRequestRepository : RepositoryBase<ProductReturnRequest>, IProductReturnRequestRepository
    {
        public ProductReturnRequestRepository(ShoppingCartContext context) : base(context)
        {
        }
    }
}
