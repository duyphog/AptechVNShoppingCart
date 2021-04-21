using System;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class PaymentTypeRepository : RepositoryBase<PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(ShoppingCartContext context) : base(context)
        {
        }
    }
}
