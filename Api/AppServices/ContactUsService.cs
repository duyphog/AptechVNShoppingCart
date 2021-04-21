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
    public class ContactUsService : ServiceBase, IContactUsService
    {
        private readonly IMapper _mapper;

        public ContactUsService(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper,
            IMapper mapper) : base(httpContextAccessor, repoWrapper)
        {
            _mapper = mapper;
        }

        public void Create(ContactUsForCreate model)
        {
            var entity = _mapper.Map<ContactUs>(model);
            _repoWrapper.ContactUs.Create(entity);
            _repoWrapper.SaveAsync();
        }

        public IEnumerable<ContactUs> GetAll()
        {
            return _repoWrapper.ContactUs.FindAll().AsEnumerable();
        }

        public void Update(ContactUsForUpdate model)
        {
            var entity = _repoWrapper.ContactUs.FindByCondition(x => x.Id == model.Id).FirstOrDefault();
            var contactUs = _mapper.Map(model, entity);

            _repoWrapper.ContactUs.Update(contactUs);
            _repoWrapper.SaveAsync();
        }
    }
}
