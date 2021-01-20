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
        public async Task<ActionResult<SimpleResponse>> Check(int token)
        {
            var ticket = await _db.tikets
                .Join(
                    _db.ticket_states,
                    t => t.state_id,
                    s => s.id,
                    (t,s) => new
                    {
                        token = t.id,
                        state = s,
                    }
                )
                .Where(t => t.token == token).FirstOrDefaultAsync();

            if (ticket != null)
            {
                return new SimpleResponse{message = ticket.state.name};
            }
            return new SimpleResponse{error = "Заявка не найдена"};
        }


        private async Task<TicketState> GetTicketState(string name)
        {
            TicketState state = await _db
                .ticket_states
                .Where(s => s.name == name)
                .FirstOrDefaultAsync();
            return state;
        }

        [HttpPost]
        public async Task<ActionResult<SimpleResponse>> New(MobileTrafficLightTicket mobile_ticket)
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
                return new SimpleResponse{error = "Светофор не найден"};
            }

            MobileUser user = await _db.mobile_users
                .Where(u => u.token == mobile_ticket.user_token).FirstOrDefaultAsync();
            if(user == null)
            {
                return new SimpleResponse{error = "Пользователь не найден"};
            }

            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            int tiket_user_today = await _db.tikets
                .Where(
                    t => t.mobile_token == mobile_ticket.user_token &&
                    t.date_add == date
                ).CountAsync();
            
            if(tiket_user_today < 5)
            {   
                if(traffic_light.district == null) 
                {   
                    return new SimpleResponse{error = "Район светофора не найден"};
                }

                string desc = mobile_ticket.description;
                if(desc == null)
                {
                    desc = "Светофор не работает";
                }

                Ticket new_ticket = 
                    new Ticket 
                    {
                        state = await GetTicketState("Поступила"),
                        type_id = 1,
                        description = desc,
                        date_add = date,
                        mobile_user = user,
                        mobile_token = mobile_ticket.user_token,
                    };

                await _db.tikets.AddAsync(new_ticket);

                await _db.ticket_traffic_lights.AddAsync(
                    new TicketTrafficLight
                    {
                        ticket_id = new_ticket.id,
                        traffic_light_id = traffic_light.id,
                    }
                );

                await _db.SaveChangesAsync();
                return new SimpleResponse{message = new_ticket.id + ""};
            }
            else
            {
                return new SimpleResponse{error = "Превышено кол-во заявок на сегодня"};
            }
        }

        //[Authorize]
        [HttpGet("TrafficLight")]
        public async Task<ActionResult<List<TicketTrafficLight>>> GetTickets()
        {
            Func<TicketTrafficLight, Ticket, TicketTrafficLight> join_ticket = 
                (l, t) => 
                {
                    l.ticket = t;
                    return l;
                };

            Func<TicketTrafficLight, TrafficLight, TicketTrafficLight> join_traffic_light = 
                (l, t) => 
                {
                    l.traffic_light = t;
                    return l;
                };

            Func<TicketTrafficLight, District, TicketTrafficLight> join_district = 
                (l, d) => 
                {
                    l.traffic_light.district = d;
                    return l;
                };

            Func<TicketTrafficLight, TicketState, TicketTrafficLight> join_state = 
                (l, s) => 
                {
                    l.ticket.state = s;
                    return l;
                };
            
            Func<TicketTrafficLight, TicketType, TicketTrafficLight> join_type = 
                (l, t) => 
                {
                    l.ticket.type = t;
                    return l;
                };
            Func<TicketTrafficLight, MobileUser, TicketTrafficLight> join_user = 
                (l, u) => 
                {
                    l.ticket.mobile_user = u;
                    return l;
                };
            // не работает не пойми почему. ПРИЧИНА???
            /*
            return _db.ticket_traffic_lights
                .Join(_db.tikets, l => l.ticket_id, t => t.id, join_ticket)
                .Join(_db.traffic_lights, l => l.traffic_light_id, t => t.id, join_traffic_light)
                .Join(_db.districts, l => l.ticket.district_id, d => d.id, join_district)
                .ToList();
            */

            // СИЛЬНЫЙ КОСТЫЛЬ
            var ticket_traffic_lights = await _db.ticket_traffic_lights.ToListAsync();
            var tikets = await _db.tikets.ToListAsync();
            var ticket_states = await _db.ticket_states.ToListAsync();
            var traffic_lights = await _db.traffic_lights.ToListAsync();
            var districts = await _db.districts.ToListAsync();
            var ticket_type = await _db.tiket_types.ToListAsync();
            var mobile_user = await _db.mobile_users.ToListAsync();
            var ticket_dublicate = await _db.ticket_dublicate.ToListAsync();

            var tickets = ticket_traffic_lights
                .Join(tikets, l => l.ticket_id, t => t.id, join_ticket)
                .Join(ticket_states, l => l.ticket.state_id, s => s.id, join_state)
                .Join(ticket_type, l => l.ticket.type_id, s => s.id, join_type)
                .Join(traffic_lights, l => l.traffic_light_id, t => t.id, join_traffic_light)
                .Join(districts, l => l.traffic_light.district_id, d => d.id, join_district)
                .Join(mobile_user, l => l.ticket.mobile_token, d => d.token, join_user)
                .ToList();

            foreach(Ticket t in tikets)
            {
                List<Ticket> dublicates = _db.ticket_dublicate
                    .Where(d => d.main_tiket == t.id)
                    .Join(_db.tikets, l => l.tiket, d => d.id, (l,d) => d)
                    .ToList();
                t.dublicate = dublicates;
            }
            // КОНЕЦ СИЛЬНОГО КОСТЫЛЯ
            return tickets;
        }

        //[Authorize]
        [HttpPost("Update")]
        public async Task<ActionResult<SimpleResponse>> UpdateState(Ticket ticket)
        {
            Ticket ticket_db = await _db.tikets.Where(t => t.id == ticket.id).FirstOrDefaultAsync();
            if(ticket_db != null)
            {
                ticket_db.state_id = ticket.state_id;
                await _db.SaveChangesAsync();
                return new SimpleResponse {message = "Данные обновлены удачно"};
            }
            return new SimpleResponse {error = "Заявка не найдена"};
        }
    }
}