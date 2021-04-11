using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.RequestModels;
using Entities.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Api.AppServices
{
    public class AppUserServices : ServiceBase, IAppUserService
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AppUserServices(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper,
            IMapper mapper, ITokenService tokenService) : base(httpContextAccessor, repoWrapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ProcessResult<IEnumerable<AppUserDTO>>> GetUsersAsync()
        {
            async Task<IEnumerable<AppUserDTO>> action()
            {
                var users = await _repoWrapper.AppUser.FindAll()
                        .Include(u => u.AppUserRoles)
                        .ThenInclude(ur => ur.Role).ToListAsync();

                return _mapper.Map<IEnumerable<AppUserDTO>>(users);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> FindUserById(Guid id)
        {
            async Task<AppUserDTO> action()
            {
                return _mapper.Map<AppUserDTO>(await _repoWrapper.AppUser.FindAppUserByIdAsync(id));
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<LoginResponse>> LoginAsync(JObject model)
        {
            async Task<LoginResponse> action()
            {
                var userName = model.GetValue("username").ToString();
                var password = model.GetValue("password").ToString();

                var user = await _repoWrapper.AppUser.FindAppUserByUserNameAsync(userName);
                if(user == null)
                    throw new InvalidOperationException("Invalid username");

                using var hmac = new HMACSHA512(user.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                        throw new InvalidOperationException("Invalid password");
                }

                return new LoginResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)
                };
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<LoginResponse>> RegisterAsync(UserRegister model)
        {
            async Task<LoginResponse> action()
            {
                var now = DateTime.Now;
                var userId = Guid.NewGuid();

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => u.UserName == model.UserName || u.Email == model.Email).CountAsync();
                if (countUserExists > 0)
                        throw new InvalidOperationException("Username or Email is exists");

                var memberRole = await _repoWrapper.AppRole.FindAppRoleByName("Member");

                using var hmac = new HMACSHA512();

                var userRoles = new List<AppUserRole>{
                    new AppUserRole { UserId = userId, RoleId = memberRole.Id }
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

                _repoWrapper.AppUser.CreateAppUser(user);
                await _repoWrapper.SaveAsync();

                return new LoginResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)
                };
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult> ChangePasswordAsync(JObject model)
        {
            async Task action()
            {
                var now = DateTime.Now;
                var passwordOld = model.GetValue("passwordOld").ToString();
                var passwordNew = model.GetValue("passwordNew").ToString();

                if (passwordOld.Equals(passwordNew))
                    throw new InvalidOperationException("Password new equals password old");

                var user = CurrentUser;
                using var hmac = new HMACSHA512(user.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordOld));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                        throw new InvalidOperationException("Invalid password");
                }

                using var hmac1 = new HMACSHA512();

                user.PasswordHash = hmac1.ComputeHash(Encoding.UTF8.GetBytes(passwordNew));
                user.PasswordSalt = hmac1.Key;
                user.ModifyDate = now;
                user.CycleCount += 1;

                _repoWrapper.AppUser.UpdateAppUser(user);
                await _repoWrapper.SaveAsync();
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> CreateAsync(UserRegister model)
        {
            async Task<AppUserDTO> action()
            {
                var now = DateTime.Now;
                var userId = Guid.NewGuid();

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => u.UserName == model.UserName || u.Email == model.Email).CountAsync();
                if (countUserExists > 0)
                    throw new InvalidOperationException("Username or Email is exists");

                var appRoles = await _repoWrapper.AppRole.FindAll().ToListAsync();

                var userRoles = new List<AppUserRole>();

                if(model.RoleIds != null)
                {
                    if (model.RoleIds.Except(appRoles.Select(r => r.Id)).Any())
                        throw new InvalidOperationException("Invalid Role");

                    model.RoleIds.ToList().ForEach(rId =>
                    {
                        userRoles.Add(new AppUserRole { UserId = userId, RoleId = rId });
                    });
                }

                using var hmac = new HMACSHA512();
                var user = new AppUser
                {
                    Id = userId,
                    UserName = model.UserName.ToLower(),
                    Email = model.Email.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("12345678")),
                    PasswordSalt = hmac.Key,
                    Status = true,
                    CreateDate = now,
                    LastActive = now,
                    CycleCount = 1,
                    AppUserRoles = userRoles
                };

                _repoWrapper.AppUser.CreateAppUser(user);
                await _repoWrapper.SaveAsync();

                return _mapper.Map<AppUserDTO>(user);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> UpdateAsync(UserUpdate model)
        {
            async Task<AppUserDTO> action()
            {
                var now = DateTime.Now;
                var user = await _repoWrapper.AppUser.FindAppUserByIdAsync(model.Id);

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => u.Email == model.Email).CountAsync();
                if (countUserExists > 0)
                    throw new InvalidOperationException("Email is exists");

                user.Email = model.Email;
                user.ModifyDate = now;
                user.CycleCount += 1;

                _repoWrapper.AppUser.UpdateAppUser(user);
                await _repoWrapper.SaveAsync();

                return _mapper.Map<AppUserDTO>(user);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult> DeleteAsync(Guid id)
        {
            async Task action()
            {
                var user = await _repoWrapper.AppUser.FindAppUserByIdAsync(id);
                if (user == null)
                    throw new InvalidOperationException("Not Find User");

                if (user.Status == false)
                    throw new InvalidOperationException("User status is false");

                user.Status = false;
                user.ModifyDate = DateTime.Now;
                user.CycleCount += 1;

                _repoWrapper.AppUser.UpdateAppUser(user);
                await _repoWrapper.SaveAsync();
            }

            return await Process.RunAsync(action);
        }
    }
}
