using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DataTransferObjects
{
    public class UserLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
