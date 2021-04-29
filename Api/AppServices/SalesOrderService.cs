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

        public async Task<ProcessResult> CreateAsync(SalesOrderForCreate salesOrder)
        {
            async Task action()
            {
                var now = DateTime.UtcNow;
                var orders = new List<SalesOrder>();
                var products = await _repoWrapper.Product.GetProductAsync();

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
                        OrderStatusId = 1,
                        //detail
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
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
                    if (x.Product.Stock - x.Quantity < 0) throw new Exception($"Stock invalid, {x.Product.ProductName} - inStock: {x.Product.Stock}");
                        x.Product.Stock -= x.Quantity;
                });

                //orders.ForEach(async x =>
                //{
                //    var product = await _repoWrapper.Product.FindProductByIdAsync(x.ProductId);
                //    product.Stock -= x.Quantity;
                //    _repoWrapper.Product.Update(product);
                //    await _repoWrapper.SaveAsync();
                //});

                if (await _repoWrapper.SaveAsync() <= 0)
                    throw new Exception("Save fail");
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<PagedList<SalesOrderDTO>>> FindAllSalesOrderAsync(SalesOrderParameters parameters)
        {
            async Task<PagedList<SalesOrderDTO>> action()
            {
                var list = await _repoWrapper.SalesOrder.FindSalesOrderAsync(parameters);
                return new PagedList<SalesOrderDTO>(_mapper.Map<List<SalesOrder>, List<SalesOrderDTO>>(list),
                   list.TotalCount, list.CurrentPages, list.PageSize);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<PagedList<SalesOrderDTO>>> FindAllSalesOrderByCurrentUser(SalesOrderParameters parameters)
        {
            async Task<PagedList<SalesOrderDTO>> action()
            {
                parameters.UserId = CurrentUser.Id;
                var list = await _repoWrapper.SalesOrder.FindSalesOrderAsync(parameters);
                return new PagedList<SalesOrderDTO>(_mapper.Map<List<SalesOrder>, List<SalesOrderDTO>>(list),
                   list.TotalCount, list.CurrentPages, list.PageSize);
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
