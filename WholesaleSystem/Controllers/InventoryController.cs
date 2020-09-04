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
    public class InventoryController : ControllerBase
    {
        public IMapper _mapper { get; set; }
        public ApplicationDbContext _context { get; set; }

        public InventoryController(IMapper mapper)
        {
            _mapper = mapper;
            _context = new ApplicationDbContext();
        }

        // GET: api/Inventory/
        [HttpGet]
        public IActionResult GetAllInventory()
        {
            var result = _context.Inventories
                .Include(x => x.PicturePaths)
                .Where(x => x.Active == true);

            return Ok(_mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryDto>>(result));
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
