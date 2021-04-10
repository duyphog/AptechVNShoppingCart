using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}
