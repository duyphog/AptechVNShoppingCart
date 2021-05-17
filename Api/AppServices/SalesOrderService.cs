using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

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

        public async Task<ProcessResult<JObject>> CreateAsync(SalesOrderForCreate salesOrder)
        {
            async Task<JObject> action()
            {
                var now = DateTime.UtcNow;
                var orders = new List<SalesOrder>();
                var products = await _repoWrapper.Product.GetProductAsync();
                var delivery = await _repoWrapper.DeliveryType.FindDeliveryByIdAsync(salesOrder.Order.DeliveryTypeId);

                salesOrder.Details.ToList().ForEach(item =>
                {
                    orders.Add(new SalesOrder
                    {
                        //master
                        AppUserId = CurrentUser.Id,
                        FirstName = salesOrder.Order.FirstName,
                        LastName = salesOrder.Order.LastName,
                        DeliveryTypeId = salesOrder.Order.DeliveryTypeId,
                        PhoneNumber = salesOrder.Order.PhoneNumber,
                        PostCode = salesOrder.Order.PostCode,
                        OrderNote = salesOrder.Order.OrderNote,
                        StreetAddress = salesOrder.Order.StreetAddress,
                        OrderDate = now,
                        City = salesOrder.Order.City,
                        CompanyName = salesOrder.Order.CompanyName,
                        Contry = salesOrder.Order.Contry,
                        OrderStatusId = 1,
                        //detail
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = products.Where(x => x.Id == item.ProductId).FirstOrDefault().Price,
                        Product = products.Where(x=>x.Id == item.ProductId).FirstOrDefault(),
                        //info
                        CreateBy = CurrentUser.UserName,
                        CreateDate = now,
                        Status = true
                    });
                });

                await _repoWrapper.SalesOrder.AddRangeSalesOrderAsync(orders);

                //update stock
                orders.ForEach(x => {
                    if (x.Product.Unlimited == false && x.Product.Stock - x.Quantity < 0) throw new InvalidOperationException($"Stock invalid, {x.Product.ProductName} - inStock: {x.Product.Stock}");
                        x.Product.Stock -= x.Quantity;
                });

                if(await _repoWrapper.SaveAsync() < 0)
                    throw new Exception("Save fail");

                var obj = new JObject
                {
                    { "orderNumber", orders[0].OrderNumber },
                    { "amount", orders.Sum(x => x.Price * x.Quantity) + delivery.Fee }
                };
                return obj;
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<PagedList<SalesOrderDTO>>> FindAllSalesOrderAsync(SalesOrderParameters parameters)
        {
            async Task<PagedList<SalesOrderDTO>> action()
            {
                var list = await _repoWrapper.SalesOrder.FindSalesOrderAsync(parameters);
                return new PagedList<SalesOrderDTO>(_mapper.Map<List<SalesOrder>, List<SalesOrderDTO>>(list),
                   list.TotalCount, list.CurrentPage, list.PageSize);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<PagedList<SalesOrderDTO>>> FindAllSalesOrderForCurrentUser(SalesOrderParameters parameters)
        {
            async Task<PagedList<SalesOrderDTO>> action()
            {
                parameters.UserId = CurrentUser.Id;
                var list = await _repoWrapper.SalesOrder.FindSalesOrderAsync(parameters);
                return new PagedList<SalesOrderDTO>(_mapper.Map<List<SalesOrder>, List<SalesOrderDTO>>(list),
                   list.TotalCount, list.CurrentPage, list.PageSize);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult> PaymentSalesOrderAsync(PaymentDetailForCreate model)
        {
            async Task action()
            {
                var payment = _mapper.Map<PaymentDetail>(model);
                payment.Id = Guid.NewGuid();
                payment.CreateBy = CurrentUser.UserName;
                payment.CreateDate = DateTime.UtcNow;

                var orders = await _repoWrapper.SalesOrder.FindSalesOrderByOrderNumberAsync(model.OrderNumber);
                orders.ToList().ForEach(x =>
                {
                    x.PaymentTypeId = model.PaymentTypeId;
                    x.IsPaid = true;
                });

                _repoWrapper.PaymentDetail.Create(payment);
                if(await _repoWrapper.SaveAsync() <= 0)
                {
                    throw new InvalidOperationException("Save fail");
                }
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<SalesOrderDTO>> TradeOrReturnAsync(SaleOrderForTradeOrReturn model)
        {
            async Task<SalesOrderDTO> action()
            {
                var order = await _repoWrapper.SalesOrder.FindSalesOrderByIdAsync(model.SalesOrderId);
                if (DateTime.UtcNow.CompareTo(order.OrderDate.AddDays(7)) > 0)
                {
                    throw new InvalidOperationException("Great than 7 Day");
                }

                order.OrderStatus = model.IsTrade ? 7 : 8;
                order.TradeReturnRequests = new List<TradeReturnRequest> {
                    new TradeReturnRequest
                    {
                        Id = Guid.NewGuid(),
                        AppUserId = CurrentUser.Id,
                        CreateBy = CurrentUser.UserName,
                        CreateDate = DateTime.UtcNow,
                        Description = model.Description,
                        ProductId = order.ProductId,
                        Quantity = model.Quantity,
                        RequestStatus = 0,
                        Type = model.IsTrade ? 0 : 1,
                        Status = true
                    }
                };

                return await _repoWrapper.SaveAsync() > 0
                    ? _mapper.Map<SalesOrderDTO>(order)
                    : throw new InvalidOperationException("Save fail");
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult> UpdateOrderStatus(JObject param)
        {
            async Task action()
            {
                var id = param.GetValue("id").ToString();
                var orderStatus = int.Parse(param.GetValue("orderStatus").ToString());

                var order = await _repoWrapper.SalesOrder.FindSalesOrderByIdAsync(id);
                if(order == null)
                {
                    throw new Exception("Id is not exist");
                }

                order.OrderStatusId = orderStatus;
                order.ModifyBy = CurrentUser.UserName;
                order.ModifyDate = DateTime.UtcNow;

                _repoWrapper.SalesOrder.Update(order);
                if (await _repoWrapper.SaveAsync() <= 0)
                    throw new Exception("Save fail");
            }

            return await Process.RunAsync(action);
        }

    }
}
