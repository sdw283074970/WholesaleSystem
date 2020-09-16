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
    [ApiController]
    public class ProductPictureController : ControllerBase
    {
        private UploadManager _uploadManager;
        private ApplicationDbContext _context;

        public ProductPictureController()
        {
            _uploadManager = new UploadManager();
            _context = new ApplicationDbContext();
        }

        // POST: api/ProductPicture/?inventoryId=foo
        [HttpPost]
        public IActionResult Post([FromQuery]int inventoryId, [FromBody]List<IFormFile> files)
        {
            var imageFiles = _uploadManager.UploadPicFile(files).ToList();
            var inventoryInDb = _context.ProdectuInventories
                .Include(x => x.PicturePaths)
                .SingleOrDefault(x => x.Id == inventoryId);

            if (inventoryInDb == null)
            {
                throw new Exception("Inventory Id: " + inventoryId + " not found.");
            }

            foreach(var i in imageFiles)
            {
                i.Inventory = inventoryInDb;
            }

            imageFiles[0].IsMainPicture = true;

            _context.ImageFiles.AddRange(imageFiles);
            _context.SaveChanges();

            return Created(Request.Path, "Upload success");
        }
    }
}
