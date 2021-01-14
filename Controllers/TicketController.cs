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

        [HttpGet("Check")]
        public async Task<ActionResult<MobileResponse>> Check(string token)
        {
            Ticket ticket = await _db.tikets.Where(t => t.token == token).FirstOrDefaultAsync();

            if (ticket != null)
            {
                return new MobileResponse{message = ticket.state.name};
            }
            return new MobileResponse{error = "Заявка не найдена"};
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
        public async Task<ActionResult<MobileResponse>> New(MobileTicket mobile_ticket)
        {

            TrafficLight traffic_light = await _db.traffic_lights
                .Where(t => t.id == mobile_ticket.traffic_light_id).FirstOrDefaultAsync();
            if(traffic_light == null)
            {
                return new MobileResponse{error = "Светофор не найден"};
            }

            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string token = hasher(mobile_ticket.traffic_light_id + mobile_ticket.user_id + date);

            Ticket ticket = await _db.tikets.Where(t => t.token == token).FirstOrDefaultAsync();
            
            if(ticket == null)
            {
                Ticket new_ticket = new Ticket 
                    {
                        token = token, state = await GetTicketState("Поступила")
                    };
                await _db.tikets.AddAsync(new_ticket);
                await _db.SaveChangesAsync();

                return new MobileResponse{message = token};
            }

            return new MobileResponse{error = "Заявка уже существует"};
        }
    }
}