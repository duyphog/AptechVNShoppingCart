using System;
namespace Entities.ResponseModels
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string[] Roles { get; set; }
    }
}
