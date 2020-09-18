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
            var resultInDb = _context.ProdectuInventories
                .Include(x => x.ImageFiles)
                .Include(x => x.ProductInventoryProductTypes)
                .ThenInclude(inventoryProducTypes => inventoryProducTypes.ProductType)
                .Where(x => x.Active == true)
                .ToList();

            var results = _mapper.Map<IList<ProductInventory>, IList<ProductInventoryDto>>(resultInDb);

            for(var i = 0; i < results.Count; i++)
            {
                foreach(var p in resultInDb[i].ProductInventoryProductTypes)
                {
                    if (p.ProductType.TypeLayer == 1)
                    {
                        results[i].ProductTypeDto = new ProductTypeDto { TypeCode = p.ProductType.TypeCode, TypeName = p.ProductType.TypeName, TypeLayer = p.ProductType.TypeLayer };
                    }
                }
            }

            return Ok(results);
        }

        // PUT: api/Inventory
        [HttpPut]
        public IActionResult SyncInventory()
        {
            var manager = new InventoryManager();
            manager.SyncInventory();
            return Ok("Sync success");
        }
    }
}
