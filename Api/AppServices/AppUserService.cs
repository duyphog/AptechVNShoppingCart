using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Api.Extentions;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.DataTransferObjects;
using Entities.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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
            async Task<AppUserDTO> action() => _mapper.Map<AppUserDTO>(await _repoWrapper.AppUser.FindAppUserByIdAsync(id));
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

                return computedHash.EqualsByteArray(user.PasswordHash)
                    ? new LoginResponse { UserName = user.UserName, Token = await _tokenService.CreateTokenAsync(user) }
                    : throw new InvalidOperationException("Invalid password");
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<LoginResponse>> RegisterAsync(AppUserForRegister user)
        {
            async Task<LoginResponse> action()
            {
                var now = DateTime.Now;
                var newId = Guid.NewGuid();

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => u.UserName.Equals(user.UserName) || u.Email.Equals(user.Email)).CountAsync();
                if (countUserExists > 0)
                        throw new InvalidOperationException("Username or Email is exists");

                var memberRole = await _repoWrapper.AppRole.FindAppRoleByName("Member");

                using var hmac = new HMACSHA512();

                var userRoles = new List<AppUserRole>{
                    new AppUserRole { UserId = newId, RoleId = memberRole.Id }
                };

                //var entity = _mapper.Map<AppUser>(user);

                var entity = new AppUser
                {
                    Id = newId,
                    UserName = user.UserName.ToLower(),
                    Email = user.Email.ToLower(),
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                    PasswordSalt = hmac.Key,
                    Status = true,
                    CreateDate = now,
                    LastActive = now,
                    Version = 1,
                    AppUserRoles = userRoles
                };

                _repoWrapper.AppUser.CreateAppUser(entity);
                var rows = await _repoWrapper.SaveAsync();
                if(rows <= 0)
                {
                    throw new InvalidOperationException("Save fail");
                }

                return new LoginResponse
                {
                    UserName = entity.UserName,
                    Token = await _tokenService.CreateTokenAsync(entity)
                };
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> CreateAsync(AppUserForRegister model)
        {
            async Task<AppUserDTO> action()
            {
                var now = DateTime.Now;
                var entityId = Guid.NewGuid();

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
                        userRoles.Add(new AppUserRole { UserId = entityId, RoleId = rId });
                    });
                }

                using var hmac = new HMACSHA512();
                var user = new AppUser
                {
                    Id = entityId,
                    UserName = model.UserName.ToLower(),
                    Email = model.Email.ToLower(),
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("12345678")),
                    PasswordSalt = hmac.Key,
                    Status = true,
                    CreateDate = now,
                    LastActive = now,
                    Version = 1,
                    AppUserRoles = userRoles
                };

                _repoWrapper.AppUser.CreateAppUser(user);
                var rows = await _repoWrapper.SaveAsync();
                if (rows <= 0)
                {
                    throw new InvalidOperationException("Save fail");
                }

                return _mapper.Map<AppUserDTO>(user);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> UpdateAsync(AppUserForUpdate model)
        {
            async Task<AppUserDTO> action()
            {
                var now = DateTime.Now;
                var entity = await _repoWrapper.AppUser.FindAppUserByIdAsync(model.Id);

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => !u.Id.Equals(model.Id) && u.Email.Equals(model.Email)).CountAsync();
                if (countUserExists > 0)
                    throw new InvalidOperationException("Email is exists");

                var user = _mapper.Map(model, entity);
                user.ModifyDate = now;
                user.Version += 1;

                _repoWrapper.AppUser.UpdateAppUser(user);
                var rows = await _repoWrapper.SaveAsync();
                if (rows <= 0)
                {
                    throw new InvalidOperationException("Save fail");
                }

                return _mapper.Map<AppUserDTO>(user);
            }

            return await Process.RunAsync(action);
        }

        public async Task<ProcessResult<AppUserDTO>> UpdateCurrentUserAsync(AppUserForUpdate model)
        {
            async Task<AppUserDTO> action()
            {
                var now = DateTime.Now;

                var countUserExists = await _repoWrapper.AppUser.FindByCondition(u => u.Id != model.Id && u.Email == model.Email).CountAsync();
                if (countUserExists > 0)
                    throw new InvalidOperationException("Email is exists");
           
                var user = _mapper.Map(model, CurrentUser);
                user.ModifyDate = now;
                user.Version += 1;

                _repoWrapper.AppUser.UpdateAppUser(user);
                var rows = await _repoWrapper.SaveAsync();
                if (rows <= 0)
                {
                    throw new InvalidOperationException("Save fail");
                }

                return _mapper.Map<AppUserDTO>(user);
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
                {
                    throw new InvalidOperationException("Password new equals password old");
                }

                using HMACSHA512 hmac = new(CurrentUser.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordOld));

                if (!computedHash.EqualsByteArray(CurrentUser.PasswordHash))
                {
                    throw new InvalidOperationException("Invalid password");
                }

                using HMACSHA512 hmac1 = new();

                CurrentUser.PasswordHash = hmac1.ComputeHash(Encoding.UTF8.GetBytes(passwordNew));
                CurrentUser.PasswordSalt = hmac1.Key;
                CurrentUser.ModifyDate = now;
                CurrentUser.Version += 1;

                _repoWrapper.AppUser.UpdateAppUser(CurrentUser);
                var rows = await _repoWrapper.SaveAsync();
                if (rows <= 0)
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
                user.ModifyDate = DateTime.Now;
                user.Version += 1;

                _repoWrapper.AppUser.UpdateAppUser(user);
                var rows = await _repoWrapper.SaveAsync();
                if (rows <= 0)
                {
                    throw new InvalidOperationException("Save fail");
                }
            }

            return await Process.RunAsync(action);
        }
    }
}
