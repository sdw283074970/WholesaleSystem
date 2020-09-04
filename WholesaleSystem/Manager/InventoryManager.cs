using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using WholesaleSystem.Models;
using WholesaleSystem.Models.ApiModels;

namespace WholesaleSystem.Manager
{
    public class InventoryManager
    {
        private ApplicationDbContext _context;

        public InventoryManager()
        {
            _context = new ApplicationDbContext();
        }

        public void SyncInventory()
        {
            var inventoryList = GetProductInventoiesFromServer();
            var inventoriesInDb = _context.Inventories.Where(x => x.Id > 0);

            foreach(var i in inventoryList)
            {
                // 如果产品已经存在，更新，否则，建立新的库存
                var inventoryInDb = _context.Inventories.SingleOrDefault(x => x.Product_barcode == i.Product_barcode);

                if (inventoryInDb != null)
                {
                    inventoryInDb.Sellable = i.Sellable;
                    inventoryInDb.Reserved = i.Reserved;
                    inventoryInDb.Shipped = i.Shipped;
                    inventoryInDb.Onway = i.Onway;
                    inventoryInDb.Product_title = i.Product_title;
                    inventoryInDb.Product_title_en = i.Product_title_en;
                    inventoryInDb.Pending = i.Pending;
                    inventoryInDb.Sold_share = i.Sold_share;
                    inventoryInDb.Warehouse_code = i.Warehouse_code;
                    inventoryInDb.Warehouse_desc = i.Warehouse_desc;
                    inventoryInDb.Pi_update_time = i.Pi_update_time;
                }
                else
                {
                    _context.Inventories.Add(new Inventory { 
                        Product_sku = i.Product_sku,
                        Reference_no = i.Reference_no,
                        Product_barcode = i.Product_barcode,
                        Sellable = i.Sellable,
                        Product_title = i.Product_title,
                        Product_title_en = i.Product_title_en,
                        Shipped = i.Shipped,
                        Reserved = i.Reserved,
                        Pending = i.Pending,
                        Shared = i.Shared,
                        Onway = i.Onway,
                        Active = true,
                        Sold_share = i.Sold_share,
                        Warehouse_code = i.Warehouse_code,
                        Unsellable = i.Unsellable,
                        Warehouse_desc = i.Warehouse_desc,
                        Pi_update_time = i.Pi_update_time
                    });
                }
            }

            _context.SaveChanges();
        }

        private bool CheckIfExistInDb(IEnumerable<Inventory> inventoriesInDb, ProductInventory inventoryInServer)
        {
            var result = inventoriesInDb.Where(x => x.Product_barcode == inventoryInServer.Product_barcode).Count();

            if (result > 0)
                return true;
            
            return false;
        }

        public IList<ProductInventory> GetProductInventoiesFromServer()
        {
            var url = "http://47.75.146.204/default/svc/web-service";
            Hashtable ht = new Hashtable();
            var inventoryList = new List<ProductInventory>();

            // 受接口限制，每次请求最多只能获得100条数据
            var currentPage = 1;

            var paramsJson = new GetProductInventoryJsonModel
            {
                Page = currentPage,
                PageSize = 100,
                Product_sku = "",
                Product_sku_arr = new string[0],
                Warehouse_code = "",
                Warehouse_code_arr = new string[0],
                Update_start_time = new DateTime(2018, 1, 1).ToString("yyyy-MM-dd hh:mm:ss")
            };

            var firstJson = Newtonsoft.Json.JsonConvert.SerializeObject(paramsJson, Newtonsoft.Json.Formatting.Indented);

            ht.Add("paramsJson", firstJson);
            ht.Add("service", "getProductInventory");

            // 验证信息 从服务器拿
            ht.Add("appToken", "5e401e82a1e7c244ec682bb31cde3706");
            ht.Add("appKey", "2ff8217b28a496c62948a16d367e59ee");

            var firstJsonStringResult = WebServiceManager.QueryPostWebService(url, ht).InnerText;
            var firstResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductInventoryJsonModel>(firstJsonStringResult);

            var totalPage = (int)Math.Ceiling((double)firstResult.Count / 100);
            inventoryList.AddRange(firstResult.Data);

            currentPage += 1;

            while (currentPage <= totalPage)
            {
                paramsJson.Page = currentPage;
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(paramsJson, Newtonsoft.Json.Formatting.Indented);
                ht["paramsJson"] = json;
                var jsonStringResult = WebServiceManager.QueryPostWebService(url, ht).InnerText;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductInventoryJsonModel>(jsonStringResult);
                inventoryList.AddRange(result.Data);

                currentPage += 1;
            }

            return inventoryList;
        }
    }
}
