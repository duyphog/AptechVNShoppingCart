using System;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ContacUsRepository : RepositoryBase<ContactUs>, IContactUsRepository
    {
        public ContacUsRepository(ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
