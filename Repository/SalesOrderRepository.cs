using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task AddRangeSalesOrderAsync(IEnumerable<SalesOrder> salesOrders)
        {
            var newOrderNumber = GetNextValueForSequence("ordernumber_seq").ToString();
            var orderNumber = new string('0', 8 - newOrderNumber.Length) + newOrderNumber;

            salesOrders.ToList().ForEach(x =>
            {
                x.Id = x.DeliveryTypeId.ToString() + x.ProductId + orderNumber;
            });

            await AddRangeAsync(salesOrders);
        }
    }
}
