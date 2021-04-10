using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IProductRepository Product { get; }

        IAppUserRepository AppUser { get; }

        IAppUserRoleRepository AppUserRole { get; }

        Task SaveAsync();
    }
}
