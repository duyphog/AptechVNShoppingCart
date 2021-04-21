using System;
namespace Entities.Models.DataTransferObjects
{
    public class ProductDTO
    {
        public string Id { get; set; }
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
        public string PhotoUrl { get; set; }
        public ProductPhotoDTO Photos { get; set; }

        public bool? Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
