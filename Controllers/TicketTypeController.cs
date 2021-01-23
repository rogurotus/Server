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
    public class TicketTypeController : ControllerBase
    {
        private PostgreDataBase _db;

        public TicketTypeController(PostgreDataBase db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<TicketType>>> GetTypes()
        {
            return await _db.tiket_types.ToListAsync();
        }
    }
}
