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

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MobileUserController : ControllerBase
    {
        private PostgreDataBase _db;

        public MobileUserController(PostgreDataBase db)
        {
            _db = db;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<SimpleResponse>> CreateAccount(MobileUser user)
        {
            MobileUser user_db = await 
                _db.mobile_users
                .Where(u => u.phone == user.phone)
                .FirstOrDefaultAsync();
            string token = PostgreDataBase.hasher(user.phone);

            if(user_db == null)
            {
                user.token = token;
                await _db.mobile_users.AddAsync(user);
                await _db.SaveChangesAsync();
                return new SimpleResponse{message = token};
            }
            // для тестов пока токен
            return new SimpleResponse{error = user_db.token};
        }
    }
}
