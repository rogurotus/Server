using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace Server.DataBase
{
    public class PostgreDataBase : DbContext
    {
        public DbSet<Ticket> tikets {get; set;}

        public DbSet<TrafficLight> traffic_lights {get; set;}
        private List<TrafficLight> test_light = new List<TrafficLight> 
        {
            new TrafficLight{id = 1, long_ = 42, lat = -42},
            new TrafficLight{id = 2, long_ = 42, lat = -42},
            new TrafficLight{id = 3, long_ = 42, lat = -42},
            new TrafficLight{id = 4, long_ = 42, lat = -42},
            new TrafficLight{id = 5, long_ = 42, lat = -42},
            new TrafficLight{id = 6, long_ = 42, lat = -42},
            new TrafficLight{id = 7, long_ = 42, lat = -42},
            new TrafficLight{id = 8, long_ = 42, lat = -42},
            new TrafficLight{id = 9, long_ = 42, lat = -42},
            new TrafficLight{id = 10, long_ = 42, lat = -42},
        };

        public DbSet<TicketState> ticket_state {get; set;}
        private List<TicketState> test_ticket_state = new List<TicketState> 
        {
            new TicketState{name = "Поступила"},
            new TicketState{name = "В обработке"},
            new TicketState{name = "Выполнена"},
        };

        public DbSet<TicketTrafficLight> ticket_traffic_lights {get; set;}
        public DbSet<OtherTicket> other_tickets {get; set;}
        
        public PostgreDataBase (DbContextOptions<PostgreDataBase> options)
            : base(options) 
        {
            Database.EnsureCreated();
            // тестовые наборы
            if(this.ticket_state.Count() > 0) {return;}

            foreach(var t in test_light)
            {
                this.traffic_lights.Add(t);
            }
            test_light.Clear();
            foreach(var s in test_ticket_state)
            {
                this.ticket_state.Add(s);
            }
            test_ticket_state.Clear();
            this.SaveChanges();
        }
    }
}