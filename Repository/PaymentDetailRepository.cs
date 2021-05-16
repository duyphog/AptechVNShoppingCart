using System;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Entities.Models.DataTransferObjects;

namespace Repository
{
    public class PaymentDetailRepository : RepositoryBase<PaymentDetail>, IPaymentDetailRepository
    {
        public PaymentDetailRepository(ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
