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
    public class PhotoController : ControllerBase
    {
        private PostgreDataBase _db;

        public PhotoController(PostgreDataBase db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<SimpleResponse> Post([FromForm]PhotoPostRequest request)
        {
            MobileUser user_db = await _db.mobile_users
                .Where(u => u.token == request.user_token)
                .FirstOrDefaultAsync();
            if (user_db == null)
            {
                return new SimpleResponse{error = "Пользователь не найден"};
            }
            Ticket ticket_db = await _db.tikets
                .Where(t => t.id == request.ticket_id)
                .FirstOrDefaultAsync();
            if (ticket_db == null)
            {
                return new SimpleResponse{error = "Заявка не найдена"};
            }

            Photo photo = new Photo();
            photo.ticket = request.ticket_id;
            using (var binaryReader = new BinaryReader(request.photo.OpenReadStream()))
            {
                photo.file = binaryReader.ReadBytes((int)request.photo.Length);
            }
            await _db.photos.AddAsync(photo);
            await _db.SaveChangesAsync();
            return new SimpleResponse{message = "Фотография прикреплена к заявке"};
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPhoto(int id)
        {
            Photo photo = await _db.photos.Where(p => p.id == id)
                .FirstOrDefaultAsync();
            if (photo == null)
            {
                return Content("Фотография не найдена");
            }
            return File(photo.file, "image/jpeg");
        }
    }
}
