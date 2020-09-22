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
            var inventoriesInDb = _context.ProductInventories.Where(x => x.Id > 0);
            foreach (var i in inventoryList)
            {
                // 如果产品已经存在就只更新
                var inventoryInDb = _context.ProductInventories.SingleOrDefault(x => x.Product_barcode == i.Product_barcode && x.Product_sku == i.Product_sku);
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
                else // 否则，建立新的库存，按照sku解析出种类归属并关联
                {
                    var typesInDb = GetProductTypeInDb(_context, i.Product_sku);
                    var newInventory = new Models.ProductInventory
                    {
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
                    };
                    _context.ProductInventories.Add(newInventory);

                    if (typesInDb == null)
                        continue;

                    foreach (var t in typesInDb)
                    {
                        var inventoryProductType = new ProductInventoryProductType
                        {
                            Inventory = newInventory,
                            ProductType = _context.ProductTypes.Find(t.Id)
                        };
                        _context.ProductInventoryProductTypes.Add(inventoryProductType);
                        newInventory.ProductInventoryProductTypes.Add(inventoryProductType);
                    }
                }
            }
            _context.SaveChanges();
        }

        private bool CheckIfExistInDb(IEnumerable<Models.ProductInventory> inventoriesInDb, Models.ApiModels.ProductInventory inventoryInServer)
        {
            var result = inventoriesInDb.Where(x => x.Product_barcode == inventoryInServer.Product_barcode).Count();

            if (result > 0)
                return true;
            
            return false;
        }

        public IList<Models.ApiModels.ProductInventory> GetProductInventoiesFromServer()
        {
            var url = "http://47.75.146.204/default/svc/web-service";
            Hashtable ht = new Hashtable();
            var inventoryList = new List<Models.ApiModels.ProductInventory>();

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
            ht.Add("appToken", "4e1ffb60d2152699500eb37d708c6330");
            ht.Add("appKey", "5426e043bd1bdc4cca58ce2ecb9f252d");

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

        public IEnumerable<ProductType> GetProductTypeInDb(ApplicationDbContext context, string sku)
        {
            var typeCodes = GetTypeCodesFromSku(sku);
            var resultList = new List<ProductType>();

            if (!typeCodes.Any())
                return null;

            for(int i = 0; i < typeCodes.Count(); i++)
            {
                var layer = i + 1;
                resultList.Add(GetOrCreateProductType(context, sku.Substring(0, layer * 2), layer));
            }

            return resultList;
        }

        public IList<string> GetTypeCodesFromSku(string sku)
        {
            var resultList = new List<string>();

            // 从sku的第0位开始判定数字
            for (int i = 0; i < sku.Length; i+=2)
            {
                var ss = sku.Substring(i, 2);

                // 如果字符串已经不是数字，需终止
                if (!IsNum(ss))
                    break;
                else // 如果是数字，则将两位字符串加入结果列表
                    resultList.Add(ss);
            }

            return resultList;
        }

        public ProductType GetOrCreateProductType(ApplicationDbContext context, string code, int layer)
        {
            var typeInDb = context.ProductTypes.SingleOrDefault(x => x.TypeCode == code);

            if (typeInDb == null)
            {
                context.ProductTypes.Add(new ProductType {
                    TypeCode = code,
                    TypeLayer = layer,
                    TypeName = "TBD",
                    IsActive = true
                });

                context.SaveChanges();

                return context.ProductTypes.SingleOrDefault(x => x.TypeCode == code);
            }
            else
            {
                return typeInDb;
            }
        }

        public bool IsNum(string text)

        {

            for (int i = 0; i < text.Length; i++)
            {

                if (!char.IsNumber(text, i))
                {

                    return false; //输入的不是数字  

                }

            }

            return true; //否则是数字

        }
    }
}
