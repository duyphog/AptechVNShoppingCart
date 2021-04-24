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
    public class PaymentTypeRepository : RepositoryBase<PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(ShoppingCartContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PaymentType>> FindAllOrderStatusAsync()
        {
            return await FindAll().OrderBy(x => x.Id).ToListAsync();
        }
    }
}
