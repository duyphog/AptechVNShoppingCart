using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Api.Extentions;
using AutoMapper;
using Contracts;
using Entities.Helpers;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Entities.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Api.AppServices
{
    public class AppUserService : ServiceBase, IAppUserService
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AppUserService(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper,
            IMapper mapper, ITokenService tokenService) : base(httpContextAccessor, repoWrapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ProcessResult<PagedList<AppUserDTO>>> GetUsersAsync(AppUserParameters parameters)
        {
            async Task<PagedList<AppUserDTO>> action()
            {
                var list = await _repoWrapper.AppUser.FindAllAppUserAsync(parameters);
                return new PagedList<AppUserDTO>(_mapper.Map<List<AppUserDTO>>(list), list.Count, list.CurrentPage, list.PageSize);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> FindUserByIdAsync(Guid id)
        {
            async Task<AppUserDTO> action() => _mapper.Map<AppUserDTO>(await _repoWrapper.AppUser.FindAppUserByIdAsync(id));
            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<LoginResponse>> LoginAsync(UserLogin model)
        {
            async Task<LoginResponse> action()
            {
                var user = await _repoWrapper.AppUser.FindAppUserByUserNameAsync(model.UserName);
                if(user == null)
                    throw new InvalidOperationException("Invalid username");

                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

                return computedHash.EqualsByteArray(user.PasswordHash)
                    ? new LoginResponse { UserName = user.UserName, Token = await _tokenService.CreateTokenAsync(user), Roles = user.AppUserRoles.Select(x => x.Role.Name).ToArray() }
                    : throw new InvalidOperationException("Invalid password");
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<LoginResponse>> RegisterAsync(AppUserForRegister model)
        {
            async Task<LoginResponse> action()
            {
                var now = DateTime.UtcNow;
                var newId = Guid.NewGuid();

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => u.UserName.Equals(model.UserName) || u.Email.Equals(model.Email)).CountAsync();
                if (countUserExists > 0)
                        throw new InvalidOperationException("Username or Email is exists");

                const string registerRoleName = "Member";
                var memberRole = await _repoWrapper.AppRole.FindAppRoleByName(registerRoleName);

                using var hmac = new HMACSHA512();

                var userRoles = new List<AppUserRole>{
                    new AppUserRole { UserId = newId, RoleId = memberRole.Id }
                };

                var user = new AppUser
                {
                    Id = newId,
                    UserName = model.UserName.ToLower(),
                    Email = model.Email.ToLower(),
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)),
                    PasswordSalt = hmac.Key,
                    Status = true,
                    CreateDate = now,
                    CreateBy = model.UserName.ToLower(),
                    LastActive = now,
                    Version = 1,
                    AppUserRoles = userRoles
                };

                _repoWrapper.AppUser.Create(user);

                return await _repoWrapper.SaveAsync() > 0
                    ? new LoginResponse { UserName = user.UserName, Token = await _tokenService.CreateTokenAsync(user), Roles = new string[] { registerRoleName } }
                    : throw new InvalidOperationException("Save fail");
                  
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> CreateAsync(AppUserForRegister model)
        {
            async Task<AppUserDTO> action()
            {
                var now = DateTime.UtcNow;
                var newId = Guid.NewGuid();

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => u.UserName.Equals(model.UserName) || u.Email.Equals(model.Email)).CountAsync();
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
                        userRoles.Add(new AppUserRole { UserId = newId, RoleId = rId });
                    });
                }

                using var hmac = new HMACSHA512();
                var user = new AppUser
                {
                    Id = newId,
                    UserName = model.UserName.ToLower(),
                    Email = model.Email.ToLower(),
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("12345678")),
                    PasswordSalt = hmac.Key,
                    Status = true,
                    CreateDate = now,
                    CreateBy = CurrentUser.UserName,
                    LastActive = now,
                    Version = 1,
                    AppUserRoles = userRoles
                };

                _repoWrapper.AppUser.Create(user);
                return await _repoWrapper.SaveAsync() > 0
                    ? _mapper.Map<AppUserDTO>(user)
                    : throw new InvalidOperationException("Save fail");
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> UpdateAsync(AppUserForUpdate model)
        {
            async Task<AppUserDTO> action()
            {
                var now = DateTime.UtcNow;
                var entity = await _repoWrapper.AppUser.FindAppUserByIdAsync(model.Id);

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => u.Id != model.Id && u.Email == model.Email).CountAsync();
                if (countUserExists > 0)
                    throw new InvalidOperationException("Email is exists");

                var user = _mapper.Map(model, entity);
                user.ModifyDate = now;
                user.ModifyBy = CurrentUser.UserName;
                user.Version += 1;

                _repoWrapper.AppUser.Update(user);
                return await _repoWrapper.SaveAsync() > 0
                    ? _mapper.Map<AppUserDTO>(user)
                    : throw new InvalidOperationException("Save fail");
        }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> UpdateCurrentUserAsync(AppUserForUpdate model)
        {
            async Task<AppUserDTO> action()
            {
                var now = DateTime.UtcNow;

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => u.Id != model.Id && u.Email == model.Email).CountAsync();
                if (countUserExists > 0)
                    throw new InvalidOperationException("Email is exists");
           
                var user = _mapper.Map(model, CurrentUser);
                user.ModifyDate = now;
                user.ModifyBy = CurrentUser.UserName;
                user.Version += 1;

                _repoWrapper.AppUser.Update(user);
                return await _repoWrapper.SaveAsync() > 0
                    ? _mapper.Map<AppUserDTO>(user)
                    : throw new InvalidOperationException("Save fail");
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult> ChangePasswordAsync(ChangePassword model)
        {
            async Task action()
            {
                var now = DateTime.UtcNow;

                if (model.PasswordOld == model.PasswordNew)
                {
                    throw new InvalidOperationException("Password new equals password old");
                }

                using HMACSHA512 hmac = new(CurrentUser.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.PasswordOld));

                if (!computedHash.EqualsByteArray(CurrentUser.PasswordHash))
                {
                    throw new InvalidOperationException("Invalid password");
                }

                using HMACSHA512 hmac1 = new();

                CurrentUser.PasswordHash = hmac1.ComputeHash(Encoding.UTF8.GetBytes(model.PasswordNew));
                CurrentUser.PasswordSalt = hmac1.Key;
                CurrentUser.ModifyBy = CurrentUser.UserName;
                CurrentUser.ModifyDate = now;
                CurrentUser.Version += 1;

                _repoWrapper.AppUser.Update(CurrentUser);
                if (await _repoWrapper.SaveAsync() <= 0)
                {
                    throw new InvalidOperationException("Save fail");
                }
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
                user.ModifyDate = DateTime.UtcNow;
                user.ModifyBy = CurrentUser.UserName;
                user.Version += 1;

                _repoWrapper.AppUser.Update(user);
                if (await _repoWrapper.SaveAsync() <= 0)
                {
                    throw new InvalidOperationException("Save fail");
                }
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> GetProfileAsync()
        {
            async Task<AppUserDTO> action() => _mapper.Map<AppUserDTO>(CurrentUser);
       
            return await Process.RunAsync(action);
        }
    }
}
