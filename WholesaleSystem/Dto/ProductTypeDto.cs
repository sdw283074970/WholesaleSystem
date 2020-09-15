using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Dto
{
    public class ProductTypeDto
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public string TypeCode { get; set; }

        public int TypeLayer { get; set; }

        public int ProductCount { get; set; }
    }
}
