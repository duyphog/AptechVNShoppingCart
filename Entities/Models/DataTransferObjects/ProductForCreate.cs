using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Entities.Models.DataTransferObjects
{
    public class ProductForCreate
    {
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int? Stock { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public bool? Unlimited { get; set; }
        public string Location { get; set; }
        public int? WarrantyPeriod { get; set; }

        public IFormFile[] Files { get; set; }
    }
}
