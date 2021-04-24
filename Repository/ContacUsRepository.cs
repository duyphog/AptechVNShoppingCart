using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ContacUsRepository : RepositoryBase<ContactUs>, IContactUsRepository
    {
        public ContacUsRepository(ShoppingCartContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<ContactUs> FindContactUsByIdAsync(Guid id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PagedList<ContactUs>> FindAllContactUsAsync(ContactUsParameters parameters)
        {
            var queries = FindAll().AsQueryable();

            queries = queries.OrderBy(x => x.Confirm).OrderBy(x => x.CreateDate);

            return await PagedList<ContactUs>.ToPagedList(queries, parameters.PageNumber, parameters.PageSize);
        }
    }
}
