using System;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
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
        protected readonly IRepositoryWrapper _repoWrapper;

        public AppUser CurrentUser => GetCurrentUser();

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

            //var userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue("name");
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue("id");

            //_appUser = _repoWrapper.AppUser.FindAppUserByUserNameAsync(userName).Result;
            _appUser = _repoWrapper.AppUser.FindAppUserByIdAsync(Guid.Parse(userId)).Result;

            return _appUser;
        }

        //protected (bool isCreate, T result) GetOrCreateEntity<T>(, Expression<Func<(T, bool)>> whereConditions = null)
        //    where T : class
        //{
        //    var isCreate = false;
        //    T result = null;

        //    if(whereConditions != null)
        //    {
        //        result = _repoWrapper.
        //    }
        //}
    }
}
