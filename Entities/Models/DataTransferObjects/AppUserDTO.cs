using System;
using System.Collections.Generic;

namespace Entities.Models.DataTransferObjects
{
    public class AppUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
    }
}
