using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WholesaleSystem.Dto;
using WholesaleSystem.Models;

namespace WholesaleSystem.Controllers
{
    [Route("api/[controller]")]
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public ProductTypeController(IMapper mapper)
        {
            _context = new ApplicationDbContext();
            _mapper = mapper;
        }

        // GET: api/ProductType/GetProductTypes
        [HttpGet]
        public IActionResult GetProductTypes()
        {
            return Ok(_mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeDto>>(_context.ProductTypes.Include(x => x.InventoryProductTypes).Where(x => x.IsActive == true)));
        }
    }
}
