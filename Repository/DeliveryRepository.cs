using System;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class DeliveryRepository : RepositoryBase<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
