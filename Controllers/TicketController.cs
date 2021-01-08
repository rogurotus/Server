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

namespace Server.Controllers
{
    public class TicketController : Controller
    {
        private PostgreDataBase _db;

        public TicketController(PostgreDataBase db)
        {
            _db = db;
        }

        public IActionResult Check(string token)
        {
            Ticket ticket = _db.tikets.Where(t => t.token == token).FirstOrDefault();

            if (ticket != null)
            {
                // поле ticket.state не подключается и state всегда null. 
                // начало костыля

                string state = _db.ticket_state.Where(s => s.id == ticket.state_id).First().name;

                // конец костыля

                return Content("{\"error\": null, \"message\": \"" + state + "\"}");
            }
            return Content("{\"error\": \"Заявка не найдена\", \"message\": null}");
        }
    }
}