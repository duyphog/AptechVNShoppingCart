using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;

namespace Contracts
{
    public interface IContactUsRepository : IRepositoryBase<ContactUs>
    {
        Task<ContactUs> FindContactUsByIdAsync(Guid id);
        Task<PagedList<ContactUs>> FindAllContactUsAsync(ContactUsParameters parameters);
    }
}
