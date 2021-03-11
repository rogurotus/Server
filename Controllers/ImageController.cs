using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
using Microsoft.AspNetCore.Authorization;


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

        //[Authorize]
        [HttpPost("{name}")]
        public async Task<SimpleResponse> Post(IFormFile file, string name)
        {
            MyImage image_bd = await _db.images.Where(i => i.name == name).FirstOrDefaultAsync();
            if (image_bd != null)
            {
                return new SimpleResponse{error = "Такая картинка уже есть"};
            }
            MyImage image = new MyImage();
            image.name = name;
            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                image.file = binaryReader.ReadBytes((int)file.Length);
            }
            await _db.images.AddAsync(image);
            await _db.SaveChangesAsync();
            return new SimpleResponse{message = name};
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> GetImage(string name)
        {
            MyImage file = await _db.images.Where(i => i.name == name).FirstOrDefaultAsync();
            if (file == null)
            {
                return Content("Картинка не найдена");
            }
            return File(file.file, "image/jpeg");
        }
    }
}
