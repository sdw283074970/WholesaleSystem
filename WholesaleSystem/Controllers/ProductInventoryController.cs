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

        // GET: api/Inventory/
        [HttpGet]
        public IActionResult GetAllProductInventory()
        {
            var resultInDb = _context.ProductInventories
                .Include(x => x.ImageFiles)
                .Include(x => x.ProductInventoryProductTypes)
                .ThenInclude(inventoryProducTypes => inventoryProducTypes.ProductType)
                .Where(x => x.Active == true)
                .ToList();

            var results = _mapper.Map<IList<ProductInventory>, IList<ProductInventoryDto>>(resultInDb);

            for(var i = 0; i < results.Count; i++)
            {
                results[i].ImageFilesDto = _mapper.Map<IList<ImageFile>, IList<ImageFileDto>>(resultInDb[i].ImageFiles.ToList());

                foreach (var p in resultInDb[i].ProductInventoryProductTypes)
                {
                    if (p.ProductType.TypeLayer == 1)
                    {
                        var coverImg = resultInDb[i].ImageFiles.SingleOrDefault(x => x.IsMainPicture == true);
                        results[i].ProductTypeDto = new ProductTypeDto { TypeCode = p.ProductType.TypeCode, TypeName = p.ProductType.TypeName, TypeLayer = p.ProductType.TypeLayer };
                        results[i].CoverImageUrl = coverImg == null ? "Images/no_image.gif" : coverImg.Url;
                    }
                }
            }

            foreach(var r in results)
            {
                if (r.ImageFilesDto == null)
                    continue;

                foreach(var i in r.ImageFilesDto)
                {
                    if (i != null)
                        r.ImageList.Add(i.Url);
                }
            }

            return Ok(results);
        }

        // GET: api/Inventory/
        [HttpGet]
        public IActionResult GetProductInventory([FromQuery]int productInventoryId)
        {
            var productInventoryInDb = _context.ProductInventories
                .Include(x => x.ImageFiles)
                .SingleOrDefault(x => x.Id == productInventoryId);

            var result = _mapper.Map<ProductInventory, ProductInventoryDto>(productInventoryInDb);

            result.ImageFilesDto = _mapper.Map<IEnumerable<ImageFile>, IEnumerable<ImageFileDto>>(productInventoryInDb.ImageFiles).ToList();

            return Ok(result);
        }

        // PUT: api/Inventory/
        [HttpPut]
        public IActionResult SyncInventory()
        {
            var manager = new InventoryManager();
            manager.SyncInventory();
            return Ok("Sync success");
        }
    }
}
