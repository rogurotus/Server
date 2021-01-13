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
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private PostgreDataBase _db;

        public TicketController(PostgreDataBase db)
        {
            _db = db;
        }

        [HttpGet("{token}")]
        public async Task<ActionResult<string>> Check(string token)
        {
            Ticket ticket = await _db.tikets.Where(t => t.token == token).FirstOrDefaultAsync();

            if (ticket != null)
            {
                // поле ticket.state не подключается и state всегда null. 
                // начало костыля

                TicketState ticket_state = await 
                    _db.ticket_state
                    .Where(s => s.id == ticket.state_id)
                    .FirstAsync();
                    
                string state = ticket_state.name;

                // конец костыля

                return "{\"error\": null, \"message\": \"" + state + "\"}";
            }
            return "{\"error\": \"Заявка не найдена\", \"message\": null}";
        }


        private async Task<TicketState> GetTicketState(string name)
        {
            TicketState state = await _db
                .ticket_state
                .Where(s => s.name == name)
                .FirstOrDefaultAsync();
            if(state == null)
            {
                state = new TicketState {name = name};
                await _db.ticket_state.AddAsync(state);
                await _db.SaveChangesAsync();
            }
            return state;
        }

        private string hasher(string data)
        {
            MD5 md5 = MD5.Create();
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));
            return BitConverter.ToString(hashData).Replace("-","");
        }

        [HttpPost]
        public async Task<ActionResult<string>> New(int id, string user_id)
        {
            //TODO проверка на существование светофора

            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string token = hasher(id + user_id + date);

            Ticket ticket = await _db.tikets.Where(t => t.token == token).FirstOrDefaultAsync();
            
            if(ticket == null)
            {
                Ticket new_ticket = new Ticket 
                    {
                        token = token, state = await GetTicketState("Поступила")
                    };
                await _db.tikets.AddAsync(new_ticket);
                await _db.SaveChangesAsync();

                return "{\"error\": null, token:\"" + token + "\"}";
            }

            return "{\"error\": \"Заявка уже существует\", \"token\": null}";
        }
    }
}