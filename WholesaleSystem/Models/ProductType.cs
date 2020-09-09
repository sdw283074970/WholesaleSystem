using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public int TypeLayer { get; set; }

        public int TypeCode { get; set; }

        public Inventory Inventory { get; set; }
    }
}
