using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Newtonsoft.Json.Linq;

namespace Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<IEnumerable<Category>> FindAllAsync();
        Task<Category> FindById(string id);
    }
}
