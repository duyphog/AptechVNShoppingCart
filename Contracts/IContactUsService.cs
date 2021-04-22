using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;

namespace Contracts
{
    public interface IContactUsService
    {
        Task<ProcessResult<PagedList<ContactUs>>> GetAllContacUsAsync(ContactUsParameters parameters);
        Task<ProcessResult<ContactUs>> FindContacUsByIdAsync(Guid id);
        Task<ProcessResult<ContactUs>> ConfirmAsync(ContactUsForConfirm model);
        Task<ProcessResult> CreateContacUsAsync(ContactUsForCreate model);
        Task<ProcessResult<ContactUs>> UpdateContacUsAsync(ContactUsForUpdate model);
    }
}
