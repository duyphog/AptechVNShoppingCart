using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ProductPhoto
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Product Product { get; set; }
    }
}
