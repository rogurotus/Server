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

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebUserController : ControllerBase
    {
        private PostgreDataBase _db;

        public WebUserController(PostgreDataBase db)
        {
            _db = db;
        }

        private string hasher(string data)
        {
            string salt = "НУ СОЛЬ ТАКАЯ НОРМАЛЬНАЯ";
            data = data + salt;
            SHA256 sha = SHA256.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(data));
            return BitConverter.ToString(hashData).Replace("-","");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<SimpleResponse>> Login(WebUser user)
        {
            string hash_pass = hasher(user.pass);
            WebUser user_db = await 
                _db.users
                .Where(u => u.pass == hash_pass && (u.login == user.login || u.email == user.email))
                .FirstOrDefaultAsync();
            
            if(user_db != null)
            {
                await Authenticate(user_db.email);
                return new SimpleResponse{message = "Вход выполнен успешно"};
            }
            return new SimpleResponse{error = "Пользователь с таким логином и паролем не найден"};
        }

        [HttpPost("Create")]
        public async Task<ActionResult<SimpleResponse>> CreateAccount(WebUser user)
        {
            string hash_pass = hasher(user.pass);
            WebUser user_db = await 
                _db.users
                .Where(u => u.login == user.login || u.email == user.email)
                .FirstOrDefaultAsync();
            
            if(user_db == null)
            {
                await _db.users.AddAsync(
                    new WebUser{login = user.login, pass = hash_pass, email = user.email});
                await _db.SaveChangesAsync();
                await Authenticate(user.email);
                return new SimpleResponse{message = "Аккаунт создан успешно"};
            }
            return new SimpleResponse{error = "Пользователь с таким логином или почтой уже существует"};
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(
                claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType
            );
            await HttpContext
                .SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(id)
                );
        }
 
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Authorization/Entrance");
        }
    }
}
