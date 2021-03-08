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
    public class TicketHistoryController : ControllerBase
    {
        private PostgreDataBase _db;

        public TicketHistoryController(PostgreDataBase db)
        {
            _db = db;
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TicketHistory>>> GetHistory(int id)
        {
            List<TicketHistory> history = await _db.ticket_historys
                .Where(h => h.ticket_id == id)
                .Join(_db.ticket_states, h => h.ticket_state_old_id, s => s.id, (h,s) => 
                new TicketHistory
                {
                    id = h.id,
                    date = h.date,
                    ticket_id = h.ticket_id,
                    ticket_state_new_id = h.ticket_state_new_id,
                    ticket_state_old_id = h.ticket_state_old_id,
                    ticket_state_old = s
                })
                .Join(_db.ticket_states, h => h.ticket_state_new_id, s => s.id, (h,s) => 
                new TicketHistory
                {
                    id = h.id,
                    date = h.date,
                    ticket_id = h.ticket_id,
                    ticket_state_new_id = h.ticket_state_new_id,
                    ticket_state_new = s,
                    ticket_state_old_id = h.ticket_state_old_id,
                    ticket_state_old = h.ticket_state_old,
                })
                .ToListAsync();
            return history;
        }
    }
}
