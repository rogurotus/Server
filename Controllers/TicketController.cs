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
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private PostgreDataBase _db;

        public TicketController(PostgreDataBase db)
        {
            _db = db;
        }

        [HttpGet("Check")]
        public async Task<ActionResult<SimpleResponse>> Check(int ticket_id)
        {
            var ticket = await _db.tikets
                .Join(
                    _db.ticket_states,
                    t => t.state_id,
                    s => s.id,
                    (t,s) => new
                    {
                        ticket_id = t.id,
                        state = s,
                    }
                )
                .Where(t => t.ticket_id == ticket_id).FirstOrDefaultAsync();

            if (ticket != null)
            {
                return new SimpleResponse{message = ticket.state.name};
            }
            return new SimpleResponse{error = "Заявка не найдена"};
        }

        //[Authorize]
        [HttpGet("{type}")]
        public async Task<ActionResult<List<TicketResponse>>> GetTickets(int type)
        {
            switch (type)
            {
                case 1:
                {
                    return (await _db.ticket_traffic_lights.ToListAsync()).Select(t => t.ToResponse(_db)).ToList();
                }
                case 2:
                {
                    return (await _db.ticket_graffitis.ToListAsync()).Select(t => t.ToResponse(_db)).ToList();
                }
                case 3:
                {
                    return (await _db.ticket_road_signs.ToListAsync()).Select(t => t.ToResponse(_db)).ToList();
                }
                case 4:
                {
                    return (await _db.ticket_button.ToListAsync()).Select(t => t.ToResponse(_db)).ToList();
                }
                default:
                {
                    return null;
                }
            }
        }

        //[Authorize]
        [HttpPost("Update")]
        public async Task<ActionResult<SimpleResponse>> UpdateState(Ticket ticket)
        {
            Ticket ticket_db = await _db.tikets
                .Where(t => t.id == ticket.id).FirstOrDefaultAsync();
            if(ticket_db != null)
            {
                if(ticket.state_id == ticket_db.state_id)
                {
                    return new SimpleResponse {error = "Заявка уже в этом состоянии"};
                }

                DateTime date_time = DateTime.Now;
                date_time = date_time.AddHours(7);
                string date = date_time.ToString("yyyy-MM-dd HH:mm:ss");
                await _db.ticket_historys.AddAsync(
                    new TicketHistory
                    {
                        ticket_id = ticket.id,
                        ticket_state_old_id = ticket_db.state_id,
                        ticket_state_new_id = ticket.state_id,
                        date = date,
                    });

                ticket_db.state_id = ticket.state_id;

                List<Ticket> dublicates = await _db.ticket_dublicate
                    .Where(d => d.main_tiket == ticket.id)
                    .Join(_db.tikets, d => d.tiket, t => t.id,
                    (d,t) => t).ToListAsync();

                foreach(Ticket t in dublicates)
                {
                    await _db.ticket_historys.AddAsync(
                    new TicketHistory
                    {
                        ticket_id = t.id,
                        ticket_state_old_id = t.state_id,
                        ticket_state_new_id = ticket.state_id,
                        date = date,
                    });
                    t.state_id = ticket.state_id;
                }

                await _db.SaveChangesAsync();
                return new SimpleResponse {message = "Данные обновлены удачно"};
            }
            return new SimpleResponse {error = "Заявка не найдена"};
        }
    }
}