using System;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ITokenService
    {
        //Task<string> CreateTokenASync(AppUser user);
        string CreateToken(AppUser user);
    }
}
