using System;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Api.AppServices
{
    public class ContactUsService : ServiceBase, IContactUsService
    {
        private readonly IMapper _mapper;

        public ContactUsService(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper,
            IMapper mapper) : base(httpContextAccessor, repoWrapper)
        {
            _mapper = mapper;
        }

        public Task<ProcessResult<ContactUs>> ConfirmAsync(ContactUsForConfirm model)
        {
            throw new NotImplementedException();
        }

        public async Task<ProcessResult> CreateContacUsAsync(ContactUsForCreate model)
        {
            async Task action()
            {
                var contactUs = _mapper.Map<ContactUs>(model);
                contactUs.Id = Guid.NewGuid();
                contactUs.CreateDate = DateTime.UtcNow;
                _repoWrapper.ContactUs.Create(contactUs);
                if (await _repoWrapper.SaveAsync() <= 0)
                    throw new Exception("Save fail");
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<ContactUs>> FindContacUsByIdAsync(Guid id)
        {
            async Task<ContactUs> action()
            {
                var contactUs = await _repoWrapper.ContactUs.FindContactUsByIdAsync(id);
                return contactUs ?? throw new Exception("Id is not exist");
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<PagedList<ContactUs>>> FindAllContacUsAsync(ContactUsParameters parameters)
        {
            async Task<PagedList<ContactUs>> action()
            {
                var list = await _repoWrapper.ContactUs.FindAllContactUsAsync(parameters);

                if (list.CurrentPage > list.TotalPages)
                    throw new Exception("CurrentPages > TotalPages");

                return list;
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<ContactUs>> UpdateContacUsAsync(ContactUsForUpdate model)
        {
            async Task<ContactUs> action()
            {
                var contactUs = await _repoWrapper.ContactUs.FindContactUsByIdAsync(model.Id);
                if (contactUs == null)
                    throw new Exception("Id is not exist");

                _ = _mapper.Map(model, contactUs);

                contactUs.ModifyBy = CurrentUser.UserName;
                contactUs.ModifyDate = DateTime.UtcNow;

                _repoWrapper.ContactUs.Update(contactUs);

                return await _repoWrapper.SaveAsync() > 0 ? contactUs : throw new Exception("Save fail");
            }

            return await Process.RunAsync(action);
        }
    }
}
