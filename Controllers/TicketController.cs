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

        private TicketState GetTicketState(string name)
        {
            TicketState state = _db
                .ticket_state
                .Where(s => s.name == name)
                .FirstOrDefault();
            if(state == null)
            {
                state = new TicketState {name = name};
                _db.ticket_state.Add(state);
            }
            return state;
        }

        private string hasher(string data)
        {
            MD5 md5 = MD5.Create();
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));
            return BitConverter.ToString(hashData).Replace("-","");
        }

        public IActionResult New(int id, string user_id)
        {
            //TODO проверка на существование светофора

            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string token = hasher(id + user_id + date);

            Ticket ticket = _db.tikets.Where(t => t.token == token).FirstOrDefault();
            
            if(ticket == null)
            {
                Ticket new_ticket = new Ticket {token = token, state = GetTicketState("Поступила")};
                _db.tikets.Add(new_ticket);
                _db.SaveChanges();

                return Content("{\"error\": null, token:\"" + token + "\"}");
            }

            return Content("{\"error\": \"Заявка уже существует\", \"token\": null}");
        }
    }
}