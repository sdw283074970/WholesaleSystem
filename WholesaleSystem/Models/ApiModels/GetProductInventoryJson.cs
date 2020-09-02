using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models.ApiModels
{
    public class GetProductInventoryJson
    {
        public int pageSize { get; set; }

        public int page { get; set; }

        public string product_sku { get; set; }

        public string[] product_sku_arr { get; set; }

        public string warehouse_code { get; set; }

        public string[] warehouse_code_arr { get; set; }

        public string update_start_time { get; set; }
    }
}
