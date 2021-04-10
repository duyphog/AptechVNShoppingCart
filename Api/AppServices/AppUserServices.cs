using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.RequestModels;
using Entities.ResponseModels;

namespace Api.AppServices
{
    public class AppUserServices : IAppUserService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;
        private readonly ITokenService _token;

        public AppUserServices(IRepositoryWrapper repoWrapper, IMapper mapper, ITokenService token)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _token = token;
        }

        public async Task<LoginResponse> CreateAsync(UserRegister model)
        {
            var now = DateTime.Now;
            var userId = Guid.NewGuid();
            var memberRoleId = Guid.Parse("7C20A959-ACF6-4CA3-8B33-49A7C94DD096");
            using var hmac = new HMACSHA512();

            var userRoles = new List<AppUserRole>{
                new AppUserRole
                {
                    UserId = userId,
                    RoleId = memberRoleId
                }
            };

            var user = new AppUser
            {
                Id = userId,
                UserName = model.UserName.ToLower(),
                Email = model.Email.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)),
                PasswordSalt = hmac.Key,
                Status = true,
                CreateDate = now,
                LastActive = now,
                CycleCount = 1,
                AppUserRoles = userRoles
            };

            _repoWrapper.AppUser.Create(user);
            await _repoWrapper.SaveAsync();

            return new LoginResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Token =_token.CreateToken(user)
            };
        }
    }
}
