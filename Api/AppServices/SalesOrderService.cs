using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Api.AppServices
{
    public class SalesOrderService : ServiceBase, ISalesOrderService
    {
        private readonly IMapper _mapper;

        public SalesOrderService(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper, IMapper mapper)
            : base(httpContextAccessor, repoWrapper)
        {
            _mapper = mapper;
        }

        public async Task<ProcessResult> CreateAsync(SalesOrderForCreate salesOrder)
        {
            async Task action()
            {
                var now = DateTime.UtcNow;
                var orders = new List<SalesOrder>();

                salesOrder.Details.ToList().ForEach(item =>
                {
                    orders.Add(new SalesOrder
                    {
                        //master
                        AppUserId = CurrentUser.Id,
                        FirstName = salesOrder.Order.FirstName,
                        LastName = salesOrder.Order.LastName,
                        PaymentTypeId = salesOrder.Order.PaymentTypeId,
                        DeliveryTypeId = salesOrder.Order.DeliveryTypeId,
                        PhoneNumber = salesOrder.Order.PhoneNumber,
                        PostCode = salesOrder.Order.PostCode,
                        OrderNote = salesOrder.Order.OrderNote,
                        StreetAddress = salesOrder.Order.StreetAddress,
                        OrderDate = now,
                        City = salesOrder.Order.City,
                        CompanyName = salesOrder.Order.CompanyName,
                        Contry = salesOrder.Order.Contry,
                        OrderStatusId = 0,
                        //detail
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        //info
                        CreateBy = CurrentUser.UserName,
                        CreateDate = now,
                        Status = true
                    });
                });

                await _repoWrapper.SalesOrder.AddRangeSalesOrderAsync(orders);
                if (await _repoWrapper.SaveAsync() <= 0)
                    throw new Exception("Save fail");
            }

            return await Process.RunAsync(action);
        }
    }
}
