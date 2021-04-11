using System;
using System.Security.Claims;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;

namespace Api.AppServices
{
    public class ServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private HttpContext _httpContext => _httpContextAccessor.HttpContext;
        private AppUser _appUser;
        public AppUser CurrentUser => GetCurrentUser();
        protected readonly IRepositoryWrapper _repoWrapper;

        public ServiceBase(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repoWrapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _repoWrapper = repoWrapper;
        }

        private AppUser GetCurrentUser()
        {
            if (!_httpContext.User.Identity.IsAuthenticated)
                return new AppUser();

            if (_appUser != null)
                return _appUser;

            var userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            _appUser = _repoWrapper.AppUser.FindAppUserByUserNameAsync(userName).Result;
            return _appUser;
        }

    }
}
