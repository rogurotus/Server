using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Docker.Models;
using Microsoft.EntityFrameworkCore;
using Server.DataBase;
using System.Security.Cryptography;
using System.Text;
using Server.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private PostgreDataBase _db;

        public ImageController(PostgreDataBase db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<SimpleResponse> Post(IFormFile file)
        {
            Image image = new Image();
            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                image.file = binaryReader.ReadBytes((int)file.Length);
            }
            await _db.images.AddAsync(image);
            await _db.SaveChangesAsync();
            return new SimpleResponse{message = "" + image.id};
        }

        [HttpGet]
        public async Task<ActionResult> GetImage(int id)
        {
            Image file = await _db.images.Where(i => i.id == id).FirstOrDefaultAsync();
            return File(file.file, "image/jpeg");
        }
    }
}
