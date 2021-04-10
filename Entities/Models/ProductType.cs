using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ProductType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
    }
}
