using System;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
