using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        // GET: api/ProductType/GetProductTypes/
        [HttpGet]
        public IActionResult GetProductTypes()
        {
            return Ok(_mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeDto>>(_context.ProductTypes.Include(x => x.InventoryProductTypes).ToList().OrderBy(x => x.TypeName)));
        }

        // GET: api/ProductType/GetProductTypeInfoById/?typeId=foo
        [HttpGet]
        public IActionResult GetProductTypeInfoById([FromQuery]int typeId)
        {
            return Ok(_mapper.Map<ProductType, ProductTypeDto>( _context.ProductTypes.SingleOrDefault(x => x.Id == typeId)));
        }

        // PUT: api/ProductType/UpdateProductTypeInfo/
        [HttpPut]
        public void UpdateProductTypeInfo([FromBody]ProductTypeDto typeInfo)
        {
            var typeInDb = _context.ProductTypes.Find(typeInfo.Id);

            typeInDb.IsActive = typeInfo.IsActive;
            typeInDb.TypeCode = typeInfo.TypeCode;
            typeInDb.TypeLayer = typeInfo.TypeLayer;
            typeInDb.TypeName = typeInfo.TypeName;

            _context.SaveChanges();
        }
    }
}
