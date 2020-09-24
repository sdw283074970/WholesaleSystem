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

        // POST: api/ProductImage/UploadImages/?productInventoryId=foo&operation=bar
        [HttpPost]
        public IActionResult UploadImages([FromQuery]int productInventoryId, [FromQuery]string operation, [FromForm(Name = "file")]List<IFormFile> files)
        {
            if (operation == "AutoParse")
            {
                var imageFiles = _uploadManager.AutoParseImages(files).ToList();
                return Created(Request.Path, _mapper.Map<IEnumerable<ImageFile>, IEnumerable<ImageFileDto>>(imageFiles));
            }
            else if (operation == "UploadToProduct")
            {
                var imageFiles = _uploadManager.UploadImagesToProduct(productInventoryId, files);
                return Created(Request.Path, _mapper.Map<IEnumerable<ImageFile>, IEnumerable<ImageFileDto>>(imageFiles));
            }

            return Ok("No operation applied");
        }

        // PUT: api/ProductImage/SetImageAsCover/?imageId=foo
        [HttpPut]
        public void SetImageAsCover([FromQuery]int imageId)
        {
            var imageInDb = _context.ImageFiles.Include(x => x.ProductInventory.ImageFiles).SingleOrDefault(x => x.Id == imageId);

            if (imageInDb == null)
                throw new Exception("Image Id " + imageId + " was not found in database.");

            foreach(var i in imageInDb.ProductInventory.ImageFiles)
            {
                i.IsMainPicture = false;
            }

            imageInDb.IsMainPicture = true;

            _context.SaveChanges();
        }

        // DELETE: api/ProductImage/SoftDeleteImage/?imageId=foo
        [HttpDelete]
        public void SoftDeleteImage([FromQuery]int imageId)
        {
            var imageInDb = _context.ImageFiles.Find(imageId);
            imageInDb.Active = false;
            imageInDb.IsMainPicture = false;
            _context.SaveChanges();
        }

        // DELETE: api/ProductImage/HardDeleteImage/?imageId=foo
        [HttpDelete]
        public void HardDeleteImage([FromQuery]int imageId)
        {
            var imageInDb = _context.ImageFiles.Find(imageId);
            imageInDb.Active = false;
            imageInDb.IsMainPicture = false;
            _context.ImageFiles.Remove(imageInDb);
            _context.SaveChanges();
        }
    }
}
