using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IAppUserRepository AppUser { get; }

        IAppUserRoleRepository AppUserRole { get; }

        IAppRoleRepository AppRole { get; }

        IProductRepository Product { get; }

        Task SaveAsync();
    }
}
