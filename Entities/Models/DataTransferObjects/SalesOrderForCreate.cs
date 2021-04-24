using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DataTransferObjects
{
    public class SalesOrderForCreate
    {
        [Required]
        public SalesOrderMasterForCreate Order { get; set; }
        [Required]
        public IEnumerable<SalesOrderDetailsForCreate> Details { get; set; }
    }
}
