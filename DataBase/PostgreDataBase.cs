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
            new TrafficLight{id =10, long_ = 42, lat = -42, district_id = 3},
        };

        public DbSet<TicketState> ticket_states {get; set;}
        private List<TicketState> test_ticket_state = new List<TicketState> 
        {
            new TicketState{name = "Поступила"},
            new TicketState{name = "В обработке"},
            new TicketState{name = "Выполнена"},
            new TicketState{name = "Отменена"},
            new TicketState{name = "Дубликат"},
        };

        public DbSet<WebUser> users {get; set;}

        public DbSet<TicketType> tiket_types {get; set;}
        private List<TicketType> test_tiket_types = new List<TicketType> 
        {
            new TicketType{id = 1, name = "Светофор", description = "Сообщить о нерабочем светофоре", url = "/Image/test1.jpg"},
            new TicketType{id = 2, name = "Граффити", description = "Сообщить о граффити в неположенном месте", url = "/Image/test2.jpg"},
            new TicketType{id = 3, name = "Дорожные знаки", description = "Сообщить о нарушениях", url = "/Image/test3.jpg"},
            new TicketType{id = 4, name = "Кнопки", description = "Сообщить о нерабочей кнопке", url = "/Image/test4.jpg"},
        };

        public DbSet<MobileUser> mobile_users {get; set;}
        private List<MobileUser> test_mobile_user = new List<MobileUser> 
        {
            new MobileUser{token = "testtoken", surname = "Иванов", name = "Иван", father_name = "Иванович", phone = "79535976614"},
        };

        public DbSet<TicketTrafficLight> ticket_traffic_lights {get; set;}

        public DbSet<TicketDuplicate> ticket_dublicate {get; set;}
        public DbSet<Image> images {get; set;}
        public DbSet<Photo> photos {get; set;}

        // TODO db на фотки блобы и вся херня
        
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
            foreach(var t in test_tiket_types)
            {
                this.tiket_types.Add(t);
            }
            foreach(var t in test_mobile_user)
            {
                this.mobile_users.Add(t);
            }
            this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(x => x.file).HasColumnType("bytea");
            });
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(x => x.file).HasColumnType("bytea");
            });
            modelBuilder.Entity<TicketDuplicate>().HasKey(d => new {d.main_tiket, d.tiket});
        }
    }
}