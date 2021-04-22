using System;
namespace Entities.Models.DataTransferObjects
{
    public class ChangePassword
    {
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
    }
}
