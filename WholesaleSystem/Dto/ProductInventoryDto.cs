﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Dto
{
    public class ProductInventoryDto
    {
        public int Id { get; set; }

        public string Product_barcode { get; set; }

        public string Product_sku { get; set; }

        public string Reference_no { get; set; }

        public string Product_title { get; set; }

        public string Product_title_en { get; set; }

        public string Warehouse_code { get; set; }

        public int Onway { get; set; }

        public IList<string> ImageList { get; set; }

        public int Pending { get; set; }

        public int Sellable { get; set; }

        public int Unsellable { get; set; }

        public int Reserved { get; set; }

        public int Shipped { get; set; }

        public int Sold_share { get; set; }

        public DateTime Pi_update_time { get; set; }

        public int Shared { get; set; }

        public string Warehouse_desc { get; set; }

        public bool Active { get; set; }

        public string CoverImageUrl { get; set; }

        public float OriginalPrice { get; set; }

        public float Price { get; set; }

        public float SalePrice { get; set; }

        public string Product_description { get; set; }

        public float CostPrice { get; set; }

        public ProductTypeDto ProductTypeDto { get; set; }

        public IList<ImageFileDto> ImageFilesDto { get; set; }

        public ProductInventoryDto()
        {
            ImageList = new List<string>();
        }
    }
}
