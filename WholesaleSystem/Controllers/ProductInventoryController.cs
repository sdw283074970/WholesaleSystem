using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WholesaleSystem.Dto;
using WholesaleSystem.Manager;
using WholesaleSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace WholesaleSystem.Controllers
{
    [Route("api/[controller]")]
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class ProductInventoryController : ControllerBase
    {
        public IMapper _mapper { get; set; }
        public ApplicationDbContext _context { get; set; }

        public ProductInventoryController(IMapper mapper)
        {
            _mapper = mapper;
            _context = new ApplicationDbContext();
        }

        // GET: api/ProductInventory/
        [HttpGet]
        public IActionResult GetAllProductInventory()
        {
            return Ok(GetProductInventories(0));
        }

        // GET: api/ProductInventory/GetProductInventoryByTypeId/?typeId=foo
        [HttpGet]
        public IActionResult GetProductInventoryByTypeId([FromQuery]int typeId)
        {
            //var results = _context.ProductInventoryProductTypes
            //    .Include(x => x.ProductType)
            //    .Include(x => x.ProductInventory)
            //    .Where(x => x.ProductType.Id == typeId && x.ProductInventory.Sellable != 0 && x.ProductInventory.Active == true);
            //var list = new List<ProductInventoryDto>();

            //foreach(var r in results)
            //{
            //    list.Add(_mapper.Map<ProductInventory, ProductInventoryDto>(r.ProductInventory));
            //}

            return Ok(GetProductInventories(typeId));
        }

        // GET: api/ProductInventory/GetProductInventory/
        [HttpGet]
        public IActionResult GetProductInventory([FromQuery]int productInventoryId)
        {
            var productInventoryInDb = _context.ProductInventories
                .Include(x => x.ImageFiles)
                .SingleOrDefault(x => x.Id == productInventoryId);

            var result = _mapper.Map<ProductInventory, ProductInventoryDto>(productInventoryInDb);

            result.ImageFilesDto = _mapper.Map<IEnumerable<ImageFile>, IEnumerable<ImageFileDto>>(productInventoryInDb.ImageFiles).ToList();

            result.ImageFilesDto = result.ImageFilesDto.Where(x => x.Active == true).ToList();

            return Ok(result);
        }

        // PUT: api/ProductInventory/SyncProductInventory
        [HttpPut]
        public IActionResult SyncProductInventory()
        {
            var manager = new InventoryManager();
            manager.SyncInventory();
            return Ok("Sync success");
        }

        // PUT: api/ProductInventory/UpdateProductInventory
        [HttpPut]
        public void UpdateProductInventory([FromQuery]int productInventoryId, [FromBody]ProductInventoryDto form)
        {
            var productInventoryInDb = _context.ProductInventories.Find(productInventoryId);
            productInventoryInDb.CostPrice = form.CostPrice;
            productInventoryInDb.SalePrice = form.SalePrice;
            productInventoryInDb.OriginalPrice = form.OriginalPrice;
            productInventoryInDb.Product_description = form.Product_description;

            _context.SaveChanges();
        }

        public IList<ProductInventoryDto> GetProductInventories(int typeId)
        {
            var productInventoryTypes = _context.ProductInventoryProductTypes
                .Include(x => x.ProductType)
                .Include(x => x.ProductInventory)
                .ThenInclude(x => x.ImageFiles)
                .Where(x => x.ProductInventory.Active == true )
                .ToList();

            if (typeId != 0)
            {
                productInventoryTypes = productInventoryTypes.Where(x => x.ProductType.Id == typeId).ToList();
            }

            var resultList = new List<ProductInventory>();

            foreach(var p in productInventoryTypes)
            {
                resultList.Add(p.ProductInventory);
            }

            var results = _mapper.Map<IList<ProductInventory>, IList<ProductInventoryDto>>(resultList);

            for (var i = 0; i < results.Count; i++)
            {
                results[i].ImageFilesDto = _mapper.Map<IList<ImageFile>, IList<ImageFileDto>>(resultList[i].ImageFiles.ToList());

                foreach (var p in resultList[i].ProductInventoryProductTypes)
                {
                    if (p.ProductType.TypeLayer == 1)
                    {
                        var coverImg = resultList[i].ImageFiles.Where(x => x.IsMainPicture == true).ToList();
                        results[i].ProductTypeDto = new ProductTypeDto { TypeCode = p.ProductType.TypeCode, TypeName = p.ProductType.TypeName, TypeLayer = p.ProductType.TypeLayer };
                        results[i].CoverImageUrl = coverImg.Count == 0 ? "Images/no_image.gif" : coverImg.First().Url;
                    }
                }
            }

            foreach (var r in results)
            {
                if (r.ImageFilesDto == null)
                    continue;

                foreach (var i in r.ImageFilesDto)
                {
                    if (i != null && i.Active == true)
                        r.ImageList.Add(i.Url);
                }
            }

            return results;
        }
    }
}
