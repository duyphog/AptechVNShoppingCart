using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DataTransferObjects
{
    public class ProductForUpdate
    {
        public string Id { get; set; }
        [Required(ErrorMessage="Product Name is valid")]
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int? Stock { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public string CategoryId { get; set; }
        public bool? Unlimited { get; set; }
        public string Location { get; set; }
        public int? WarrantyPeriod { get; set; }
        public bool? Status { get; set; }
    }
}
