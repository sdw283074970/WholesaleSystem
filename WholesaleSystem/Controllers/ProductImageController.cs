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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WholesaleSystem.Controllers
{
    [Route("api/[controller]")]
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private UploadManager _uploadManager;
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public ProductImageController(IMapper mapper)
        {
            _uploadManager = new UploadManager();
            _context = new ApplicationDbContext();
            _mapper = mapper;
        }

        // POST: api/ProductImage/
        [HttpPost]
        public IActionResult UploadImages([FromForm(Name = "file")]List<IFormFile> files)
        {
            var imageFiles = _uploadManager.HandleUploadedPicFile(files).ToList();

            return Created(Request.Path, _mapper.Map<IEnumerable<ImageFile>, IEnumerable<ImageFileDto>>(imageFiles));
        }
    }
}
