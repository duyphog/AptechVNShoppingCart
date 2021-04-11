using System;
using System.Collections.Generic;

namespace Entities.Models.DTOs
{
    public class AppUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
