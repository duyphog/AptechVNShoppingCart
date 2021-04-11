using System;
namespace Entities.Models.RequestModels
{
    public class UserUpdate : UserRegister
    {
        public Guid Id { get; set; }
    }
}
