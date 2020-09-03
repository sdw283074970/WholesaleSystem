using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WholesaleSystem.Manager;

namespace WholesaleSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        public IMapper _mapper { get; set; }
        public InventoryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: api/Inventory
        [HttpGet]
        public IActionResult Get()
        {
            var manager = new InventoryManager();

            manager.SyncInventory();

            return Ok("Sync success");
        }
    }
}
