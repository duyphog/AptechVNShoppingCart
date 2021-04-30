using System;
namespace Entities.Models.DataTransferObjects
{
    public class ProductPhotoDTO
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}
