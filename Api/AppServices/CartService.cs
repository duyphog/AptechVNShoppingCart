using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Api.AppServices
{
    public class CartService : ServiceBase, ICartService
    {
        private readonly IMapper _mapper;

        public CartService(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper,
            IMapper mapper) : base(httpContextAccessor, repoWrapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<CartDTO> GetAll()
        {
            var carts = _repoWrapper.SalesOrder.FindAll().ToList();

            return _mapper.Map<IEnumerable<CartDTO>>(carts);
        }

        public void CreateCart(CartViewModel model)
        {
            var salesOrderId = Guid.NewGuid();
            var now = DateTime.Now;
            var salesOrder = _mapper.Map<SalesOrder>(model.SalesOrder);
            var details = new List<SalesOrderDetail>();

            decimal totalAmount = 0;

            model.Details.ToList().ForEach(x =>
            {
                totalAmount += x.Quantity * x.Price;
                details.Add(new SalesOrderDetail
                {
                    Id = Guid.NewGuid(),
                    SalesOrderId = salesOrderId,
                    Record = x.Record,
                    ProductId = x.ProductId,
                    Price = x.Price,
                    Quantity = x.Quantity
                });
            });

            salesOrder.OrderNumber = "";
            salesOrder.OrderStatusId = 1;
            salesOrder.AppUserId = CurrentUser.Id;
            salesOrder.OrderDate = now;
            salesOrder.SalesOrderDetails = details;

            _repoWrapper.SalesOrder.Create(salesOrder);
            _repoWrapper.SaveAsync();
        }

        public void UpdateCart(CartViewModel model)
        {
            var entity = _repoWrapper.SalesOrder.FindByCondition(x => x.Id == model.SalesOrder.Id).FirstOrDefault();
            if (entity == null)
                throw new InvalidOperationException("Id invalid");

            var now = DateTime.Now;
            var details = new List<SalesOrderDetail>();

            decimal totalAmount = 0;
            var salesOrder = _mapper.Map(model.SalesOrder, entity);

            salesOrder.SalesOrderDetails.Clear();

            model.Details.ToList().ForEach(x =>
            {
                totalAmount += x.Quantity * x.Price;
                details.Add(new SalesOrderDetail
                {
                    Id = Guid.NewGuid(),
                    SalesOrderId = salesOrder.Id,
                    Record = x.Record,
                    ProductId = x.ProductId,
                    Price = x.Price,
                    Quantity = x.Quantity
                });
            });

            salesOrder.SalesOrderDetails = details;

            _repoWrapper.SalesOrder.Update(salesOrder);
            _repoWrapper.SaveAsync();
        }
    }
}
