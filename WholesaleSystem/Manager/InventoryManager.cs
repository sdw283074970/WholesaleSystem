using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using WholesaleSystem.Models.ApiModels;

namespace WholesaleSystem.Manager
{
    public class InventoryManager
    {
        public void SyncInventory()
        {
            var url = "http://47.75.146.204/default/svc/web-service";
            Hashtable ht = new Hashtable();

            var paramsJson = new GetProductInventoryJson { 
                Page = 1,
                PageSize = 1000,
                Product_sku = "",
                Product_sku_arr = new string[0],
                Warehouse_code = "",
                Warehouse_code_arr = new string[0],
                Update_start_time = new DateTime(2018, 1, 1)
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(paramsJson, Newtonsoft.Json.Formatting.Indented);

            ht.Add("paramsJson", json);
            ht.Add("appToken", "5e401e82a1e7c244ec682bb31cde3706");
            ht.Add("appKey", "2ff8217b28a496c62948a16d367e59ee");
            ht.Add("service", "getProductInventory");

            var xx = WebServiceManager.QueryPostWebService(url, ht);
        }
    }
}
