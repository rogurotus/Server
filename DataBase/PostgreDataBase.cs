using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace Server.DataBase
{
    public class PostgreDataBase : DbContext
    {
        public DbSet<District> districts {get; set;}
        private static List<District> test_district = new List<District> 
        {
            new District{id = 1, name = "Железнодорожный"},
            new District{id = 2, name = "Кировский"},
            new District{id = 3, name = "Ленинский"},
            new District{id = 4, name = "Октябрьский"},
            new District{id = 5, name = "Свердловский"},
            new District{id = 6, name = "Советский"},
            new District{id = 7, name = "Центральный"},
        };
        public DbSet<Ticket> tikets {get; set;}

        public DbSet<TrafficLight> traffic_lights {get; set;}
        private List<TrafficLight> test_light = new List<TrafficLight> 
        {
            new TrafficLight{id = 1, long_ = 42, lat = -42, district_id = 1},
            new TrafficLight{id = 2, long_ = 42, lat = -42, district_id = 2},
            new TrafficLight{id = 3, long_ = 42, lat = -42, district_id = 3},
            new TrafficLight{id = 4, long_ = 42, lat = -42, district_id = 4},
            new TrafficLight{id = 5, long_ = 42, lat = -42, district_id = 5},
            new TrafficLight{id = 6, long_ = 42, lat = -42, district_id = 6},
            new TrafficLight{id = 7, long_ = 42, lat = -42, district_id = 7},
            new TrafficLight{id = 8, long_ = 42, lat = -42, district_id = 1},
            new TrafficLight{id = 9, long_ = 42, lat = -42, district_id = 2},
            new TrafficLight{id = 10, long_ = 42, lat = -42, district_id = 3},
        };

        public DbSet<TicketState> ticket_states {get; set;}
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
            if(this.ticket_states.Count() > 0) {return;}

            foreach(var d in test_district)
            {
                this.districts.Add(d);
            }
            foreach(var t in test_light)
            {
                this.traffic_lights.Add(t);
            }
            foreach(var s in test_ticket_state)
            {
                this.ticket_states.Add(s);
            }

            this.SaveChanges();
        }

/*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OtherTicket>()
                .HasNoKey();

            modelBuilder.Entity<TicketTrafficLight>()
                .HasNoKey();
        }
*/
    }
}