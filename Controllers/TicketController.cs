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
        public async Task<ActionResult<SimpleResponse>> Check(string token)
        {
            var ticket = await _db.tikets
                .Join(
                    _db.ticket_states,
                    t => t.state_id,
                    s => s.id,
                    (t,s) => new
                    {
                        token = t.token,
                        state = s,
                    }
                )
                .Where(t => t.token == token).FirstOrDefaultAsync();

            if (ticket != null)
            {
                return new MobileResponse{message = ticket.state.name};
                return new SimpleResponse{message = ticket.state.name};
            }
            return new MobileResponse{error = "Заявка не найдена"};
            return new SimpleResponse{error = "Заявка не найдена"};
        }


        private async Task<TicketState> GetTicketState(string name)
        {
            TicketState state = await _db
                .ticket_states
                .Where(s => s.name == name)
                .FirstOrDefaultAsync();
            if(state == null)
            {
                state = new TicketState {name = name};
                await _db.ticket_states.AddAsync(state);
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
        public async Task<ActionResult<SimpleResponse>> New(MobileTicket mobile_ticket)
        {
            var traffic_light = await _db.traffic_lights
                .Join(
                    _db.districts, 
                    t => t.district_id, 
                    d => d.id, 
                    (t,d) => new
                    {
                        id = t.id,
                        district = d,
                    })
                .Where(t => t.id == mobile_ticket.traffic_light_id)
                .FirstOrDefaultAsync();
            if(traffic_light == null)
            {
                return new MobileResponse{error = "Светофор не найден"};
                return new SimpleResponse{error = "Светофор не найден"};
            }

            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string token = hasher(mobile_ticket.traffic_light_id + mobile_ticket.user_id + date);
            Ticket ticket = await _db.tikets.Where(t => t.token == token).FirstOrDefaultAsync();

            if(ticket == null)
            {   
                if(traffic_light.district == null) {return new MobileResponse{error = "Район светофора не найден"};}
                if(traffic_light.district == null) {return new SimpleResponse{error = "Район светофора не найден"};}

                Ticket new_ticket = new Ticket 
                    {
                        token = token, 
                        state = await GetTicketState("Поступила"),
                        district = traffic_light.district,
                    };
                await _db.tikets.AddAsync(new_ticket);

                string desc = mobile_ticket.description;
                if(desc == null)
                {
                    desc = "Светофор не работает";
                }
                await _db.ticket_traffic_lights.AddAsync(
                    new TicketTrafficLight
                    {
                        ticket_id = new_ticket.id,
                        description = desc,
                        traffic_light_id = traffic_light.id,
                    }
                );

                await _db.SaveChangesAsync();
                return new MobileResponse{message = token};
                return new SimpleResponse{message = token};
            }

            return new MobileResponse{error = "Заявка уже существует"};
            return new SimpleResponse{error = "Заявка уже существует"};
        }
    }
}