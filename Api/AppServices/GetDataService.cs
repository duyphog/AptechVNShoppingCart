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
    public class GetDataService : ServiceBase, IGetDataService
    {
        private readonly IMapper _mapper;

        public GetDataService(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper,
            IMapper mapper, ITokenService tokenService) : base(httpContextAccessor, repoWrapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<OrderStatusDTO> GetOrderStatus()
        {
            var lst = _repoWrapper.OrderStatus.FindAll().AsEnumerable();
            return _mapper.Map<IEnumerable<OrderStatusDTO>>(lst);
        }

        public IEnumerable<PaymentTypeDTO> GetPaymentType()
        {
            var lst = _repoWrapper.PaymentType.FindAll().AsEnumerable();
            return _mapper.Map<IEnumerable<PaymentTypeDTO>>(lst);
        }
    }
}
