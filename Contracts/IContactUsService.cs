using System;
using System.Collections.Generic;
using Entities.Models;
using Entities.Models.DataTransferObjects;

namespace Contracts
{
    public interface IContactUsService
    {
        IEnumerable<ContactUs> GetAll();
        void Create(ContactUsForCreate model);
        void Update(ContactUsForUpdate model);
    }
}
