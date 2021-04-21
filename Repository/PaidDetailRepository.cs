using System;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class PaidDetailRepository : RepositoryBase<PaidDetail>, IPaidDetailRepository
    {
        public PaidDetailRepository(ShoppingCartContext context) : base(context)
        {
        }
    }
}
