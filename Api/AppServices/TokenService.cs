using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.AppServices
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IRepositoryWrapper _repoWrapper;

        public TokenService(IConfiguration config, IRepositoryWrapper repoWrapper)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtConfig:Secret"]));
            _repoWrapper = repoWrapper;
        }


        public async Task<string> CreateTokenAsync(AppUser user)
        {
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var roles = await _repoWrapper.AppUserRole.GetRolesByUserId(user.Id);


            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x.Name)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
