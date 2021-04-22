using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DataTransferObjects
{
    public class CategoryForCreate
    {
        [Required]
        [MaxLength(2, ErrorMessage ="ID Maxlengh = 2")]
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
