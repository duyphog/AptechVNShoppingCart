using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PagedList<SalesOrder>> FindSalesOrderAsync(SalesOrderParameters parameters)
        {
            var queries = FindAll().AsQueryable();

            if (parameters.UserId != null)
            {
                queries = queries.Where(x => x.AppUserId == parameters.UserId);
            }
            queries = queries.OrderBy(x => x.OrderDate).OrderBy(x => x.Id);

            return await PagedList<SalesOrder>.ToPagedList(queries, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<SalesOrder> FindSalesOrderByIdAsync(string id) => await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        
    }
}
