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
    public class CreateTicketController : ControllerBase
    {
        private PostgreDataBase _db;

        public CreateTicketController(PostgreDataBase db)
        {
            _db = db;
        }

        [HttpPost("QRTrafficLight")]
        public async Task<ActionResult<SimpleResponse>> QRNew(QRTrafficLightTicketRequest mobile_ticket)
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

            DateTime date_time = DateTime.Now;
            date_time = date_time.AddHours(7);
            string date = date_time.ToString("yyyy-MM-dd HH:mm:ss");

            int ticket_user_today = await _db.tikets
                .Where(
                    t => t.mobile_token == mobile_ticket.user_token &&
                    t.date_add.Substring(0,10) == date.Substring(0,10)
                ).CountAsync();

            //ALARM
            ticket_user_today = 0;
            //ALARM
            
            if(ticket_user_today < 5)
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
                        state_id = 1,
                        type_id = 1,
                        description = desc,
                        date_add = date,
                        mobile_token = mobile_ticket.user_token,
                        long_ = traffic_light.long_,
                        lat = traffic_light.lat,
                    };

                await _db.tikets.AddAsync(new_ticket);
                await _db.SaveChangesAsync();
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


        [HttpPost("TrafficLight")]
        public async Task<ActionResult<SimpleResponse>> CustomNew(CustomTrafficLightTicket mobile_ticket)
        {
            MobileUser user = await _db.mobile_users
                .Where(u => u.token == mobile_ticket.user_token).FirstOrDefaultAsync();
            if(user == null)
            {
                return new SimpleResponse{error = "Пользователь не найден"};
            }

            DateTime date_time = DateTime.Now;
            date_time = date_time.AddHours(7);
            string date = date_time.ToString("yyyy-MM-dd HH:mm:ss");
            int ticket_user_today = await _db.tikets
                .Where(
                    t => t.mobile_token == mobile_ticket.user_token &&
                    t.date_add.Substring(0,10) == date.Substring(0,10)
                ).CountAsync();

            //ALARM
            ticket_user_today = 0;
            //ALARM
            
            if(ticket_user_today < 5)
            {   
                string desc = mobile_ticket.description;
                if(desc == null)
                {
                    desc = "Светофор не работает";
                }

                Ticket new_ticket = 
                    new Ticket 
                    {
                        state_id = 1,
                        type_id = 1,
                        description = desc,
                        date_add = date,
                        mobile_token = mobile_ticket.user_token,
                        long_ = mobile_ticket.long_,
                        lat = mobile_ticket.lat,
                    };

                await _db.tikets.AddAsync(new_ticket);
                await _db.SaveChangesAsync();

                await _db.ticket_traffic_lights.AddAsync(
                    new TicketTrafficLight
                    {
                        ticket_id = new_ticket.id,
                        traffic_light_id = 1,
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

        [HttpPost("Button")]
        public async Task<ActionResult<SimpleResponse>> CustomNew(CustomButtonTicket mobile_ticket)
        {
            MobileUser user = await _db.mobile_users
                .Where(u => u.token == mobile_ticket.user_token).FirstOrDefaultAsync();
            if(user == null)
            {
                return new SimpleResponse{error = "Пользователь не найден"};
            }

            DateTime date_time = DateTime.Now;
            date_time = date_time.AddHours(7);
            string date = date_time.ToString("yyyy-MM-dd HH:mm:ss");
            int ticket_user_today = await _db.tikets
                .Where(
                    t => t.mobile_token == mobile_ticket.user_token &&
                    t.date_add.Substring(0,10) == date.Substring(0,10)
                ).CountAsync();

            //ALARM
            ticket_user_today = 0;
            //ALARM
            
            if(ticket_user_today < 5)
            {   
                string desc = mobile_ticket.description;
                if(desc == null)
                {
                    desc = "Светофор не работает";
                }

                Ticket new_ticket = 
                    new Ticket 
                    {
                        state_id = 1,
                        type_id = 4,
                        description = desc,
                        date_add = date,
                        mobile_token = mobile_ticket.user_token,
                        long_ = mobile_ticket.long_,
                        lat = mobile_ticket.lat,
                    };

                await _db.tikets.AddAsync(new_ticket);
                await _db.SaveChangesAsync();

                await _db.ticket_button.AddAsync(
                    new TicketButton
                    {
                        ticket_id = new_ticket.id,
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

        [HttpPost("Graffiti")]
        public async Task<ActionResult<SimpleResponse>> CustomNew(CustomGraffitiTicket mobile_ticket)
        {
            MobileUser user = await _db.mobile_users
                .Where(u => u.token == mobile_ticket.user_token).FirstOrDefaultAsync();
            if(user == null)
            {
                return new SimpleResponse{error = "Пользователь не найден"};
            }

            DateTime date_time = DateTime.Now;
            date_time = date_time.AddHours(7);
            string date = date_time.ToString("yyyy-MM-dd HH:mm:ss");
            int ticket_user_today = await _db.tikets
                .Where(
                    t => t.mobile_token == mobile_ticket.user_token &&
                    t.date_add.Substring(0,10) == date.Substring(0,10)
                ).CountAsync();

            //ALARM
            ticket_user_today = 0;
            //ALARM
            
            if(ticket_user_today < 5)
            {   
                string desc = mobile_ticket.description;
                if(desc == null)
                {
                    desc = "Светофор не работает";
                }

                Ticket new_ticket = 
                    new Ticket 
                    {
                        state_id = 1,
                        type_id = 2,
                        description = desc,
                        date_add = date,
                        mobile_token = mobile_ticket.user_token,
                        long_ = mobile_ticket.long_,
                        lat = mobile_ticket.lat,
                    };

                await _db.tikets.AddAsync(new_ticket);
                await _db.SaveChangesAsync();

                await _db.ticket_graffitis.AddAsync(
                    new TicketGraffiti
                    {
                        ticket_id = new_ticket.id,
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

        [HttpPost("RoadSign")]
        public async Task<ActionResult<SimpleResponse>> CustomNew(CustomRoadSignTicket mobile_ticket)
        {
            MobileUser user = await _db.mobile_users
                .Where(u => u.token == mobile_ticket.user_token).FirstOrDefaultAsync();
            if(user == null)
            {
                return new SimpleResponse{error = "Пользователь не найден"};
            }

            DateTime date_time = DateTime.Now;
            date_time = date_time.AddHours(7);
            string date = date_time.ToString("yyyy-MM-dd HH:mm:ss");
            int ticket_user_today = await _db.tikets
                .Where(
                    t => t.mobile_token == mobile_ticket.user_token &&
                    t.date_add.Substring(0,10) == date.Substring(0,10)
                ).CountAsync();

            //ALARM
            ticket_user_today = 0;
            //ALARM
            
            if(ticket_user_today < 5)
            {   
                string desc = mobile_ticket.description;
                if(desc == null)
                {
                    desc = "Светофор не работает";
                }

                Ticket new_ticket = 
                    new Ticket 
                    {
                        state_id = 1,
                        type_id = 3,
                        description = desc,
                        date_add = date,
                        mobile_token = mobile_ticket.user_token,
                        long_ = mobile_ticket.long_,
                        lat = mobile_ticket.lat,
                    };

                await _db.tikets.AddAsync(new_ticket);
                await _db.SaveChangesAsync();

                await _db.ticket_road_signs.AddAsync(
                    new TicketRoadSign
                    {
                        ticket_id = new_ticket.id,
                        ticket = new_ticket,
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
    }
}