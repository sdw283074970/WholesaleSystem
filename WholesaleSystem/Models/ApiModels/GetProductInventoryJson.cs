using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models.ApiModels
{
    public class GetProductInventoryJson
    {
        public int PageSize { get; set; }

        public int Page { get; set; }

        public string Product_sku { get; set; }

        public string[] Product_sku_arr { get; set; }

        public string Warehouse_code { get; set; }

        public string[] Warehouse_code_arr { get; set; }

        public DateTime Update_start_time { get; set; }
    }
}
