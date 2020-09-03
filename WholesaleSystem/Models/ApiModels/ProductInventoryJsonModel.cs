using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models.ApiModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ProductInventoryJsonModel : ResponseJsonModel
    {
        public IList<ProductInventory> Data { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ProductInventory
    {
        public string Product_barcode { get; set; }

        public string Product_sku { get; set; }

        public string Reference_no { get; set; }

        public string Product_title { get; set; }

        public string Product_title_en { get; set; }

        public string Warehouse_code { get; set; }

        public int Onway { get; set; }

        public int Pending { get; set; }

        public int Sellable { get; set; }

        public int Unsellable { get; set; }

        public int Reserved { get; set; }

        public int Shipped { get; set; }

        public int Sold_share { get; set; }

        public DateTime Pi_update_time { get; set; }

        public int Shared { get; set; }

        public string Warehouse_desc { get; set; }
    }
}
