using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models.ApiModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetProductInventoryJsonModel
    {
        public int PageSize { get; set; }

        public int Page { get; set; }

        public string Product_sku { get; set; }

        public string[] Product_sku_arr { get; set; }

        public string Warehouse_code { get; set; }

        public string[] Warehouse_code_arr { get; set; }

        public string Update_start_time { get; set; }
    }
}
