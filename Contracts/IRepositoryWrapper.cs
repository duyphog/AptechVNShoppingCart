using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IProductRepository Product { get; }

        Task SaveAsync();
    }
}
