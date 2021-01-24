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

        [HttpPost("QR")]
        public async Task<ActionResult<SimpleResponse>> QRNew(TrafficLightTicketRequest mobile_ticket)
        {
            
            var traffic_light = await _db.traffic_lights
                .Where(t => t.hash_code == mobile_ticket.traffic_light_hash_code)
                .FirstOrDefaultAsync();
            if(traffic_light == null)
            {
                return new SimpleResponse{error = "Светофор не найден"};
            }

            // Костыль заджоинил район
            var _district = await _db.districts
                .Where(d => d.id == traffic_light.district_id).FirstOrDefaultAsync();
            traffic_light.district = _district;

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
            var tickets = await _db.tikets.ToListAsync();
            var ticket_states = await _db.ticket_states.ToListAsync();
            var traffic_lights = await _db.traffic_lights.ToListAsync();
            var districts = await _db.districts.ToListAsync();
            var ticket_type = await _db.tiket_types.ToListAsync();
            var mobile_user = await _db.mobile_users.ToListAsync();
            var ticket_dublicate = await _db.ticket_dublicate.ToListAsync();
            
            var traffic_light_join = traffic_lights
                .Join(districts, tr => tr.district_id, d => d.id, PostgreDataBase.join_district);

            foreach(Ticket t in tickets)
            {
                List<int> dublicates = await _db.ticket_dublicate
                    .Where(d => d.main_tiket == t.id)
                    .Select(t => t.tiket)
                    .ToListAsync();
                t.dublicates_id = dublicates;
            }

            var tickets_join = tickets
                .Join(ticket_type, t => t.type_id, ty => ty.id, PostgreDataBase.join_type)
                .Join(ticket_states, t => t.state_id, st => st.id, PostgreDataBase.join_state)
                .Join(mobile_user, t => t.mobile_token, m => m.token, PostgreDataBase.join_user);

            var tickets_res = ticket_traffic_lights
                .Join(tickets_join, l => l.ticket_id, t => t.id, PostgreDataBase.join_ticket)
                .Join(traffic_lights, l => l.traffic_light_id, t => t.id, PostgreDataBase.join_traffic_light)
                .ToList();
            // КОНЕЦ СИЛЬНОГО КОСТЫЛЯ
            return tickets_res;
        }

        //[Authorize]
        [HttpPost("Update")]
        public async Task<ActionResult<SimpleResponse>> UpdateState(Ticket ticket)
        {
            Ticket ticket_db = await _db.tikets
                .Where(t => t.id == ticket.id).FirstOrDefaultAsync();
            if(ticket_db != null)
            {
                ticket_db.state_id = ticket.state_id;

                List<Ticket> dublicates = await _db.ticket_dublicate
                    .Where(d => d.main_tiket == ticket.id)
                    .Join(_db.tikets, d => d.tiket, t => t.id,
                    (d,t) => t).ToListAsync();

                foreach(Ticket t in dublicates)
                {
                    t.state_id = ticket.state_id;
                }

                await _db.SaveChangesAsync();
                return new SimpleResponse {message = "Данные обновлены удачно"};
            }
            return new SimpleResponse {error = "Заявка не найдена"};
        }

        [HttpGet("Photos")]
        public async Task<ActionResult<List<int>>> GetPhotosTicket(int id)
        {
            List<int> photos = await _db.photos
                .Where(p => p.ticket == id).Select(p => p.id).ToListAsync();
            return photos;
        }
    }
}