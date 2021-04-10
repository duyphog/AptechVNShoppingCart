using System;
namespace Entities.ResponseModels
{
    public class LoginResponse
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }
    }
}
