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
    public class AppUtilsService : ServiceBase, IAppUtilsService
    {
        private readonly IMapper _mapper;

        public AppUtilsService(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper,
            IMapper mapper, ITokenService tokenService) : base(httpContextAccessor, repoWrapper)
        {
            _mapper = mapper;
        }

        public async Task<ProcessResult<IEnumerable<DeliveryTypeDTO>>> FindAllDeliveryTypeAsync()
        {
            async Task<IEnumerable<DeliveryTypeDTO>> action()
            {
                var lst = await _repoWrapper.DeliveryType.FindAllDeliveryTypeAsync();
                return _mapper.Map<IEnumerable<DeliveryTypeDTO>>(lst);
            }
            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<IEnumerable<OrderStatusDTO>>> FindAllOrderStatusAsync()
        {
            async Task<IEnumerable<OrderStatusDTO>> action()
            {
                var lst = await _repoWrapper.OrderStatus.GetAllOrderStatusAsync();
                return _mapper.Map<IEnumerable<OrderStatusDTO>>(lst);
            }
            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<IEnumerable<PaymentTypeDTO>>> FindAllPaymentTypeAsync()
        {
            
            async Task<IEnumerable<PaymentTypeDTO>> action()
            {
                var lst = await _repoWrapper.PaymentType.FindAllOrderStatusAsync();
                return _mapper.Map<IEnumerable<PaymentTypeDTO>>(lst);
            }
            return await Process.RunAsync(action);
        }


    }
}
