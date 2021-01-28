using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Server.DataBase
{
    public class PostgreDataBase : DbContext
    {
        public static Func<TicketTrafficLight, Ticket, TicketTrafficLight> join_ticket = 
            (l, t) => 
            {
                l.ticket = t;
                return l;
            };

        public static Func<TicketTrafficLight, TrafficLight, TicketTrafficLight> join_traffic_light = 
            (l, t) => 
            {
                l.traffic_light = t;
                return l;
            };

        public static Func<TrafficLight, District, TrafficLight> join_district = 
            (l, d) => 
            {
                l.district = d;
                return l;
            };

        public static Func<Ticket, TicketState, Ticket> join_state = 
            (l, s) => 
            {
                l.state = s;
                return l;
            };
            
        public static Func<Ticket, TicketType, Ticket> join_type = 
            (l, t) => 
            {
                l.type = t;
                return l;
            };
        public static Func<Ticket, MobileUser, Ticket> join_user = 
            (l, u) => 
            {
                l.mobile_user = u;
                return l;
            };

        public static string hasher(string data)
        {
            string salt = "НУ СОЛЬ ТАКАЯ НОРМАЛЬНАЯ";
            data = data + salt;
            SHA256 sha = SHA256.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(data));
            return BitConverter.ToString(hashData).Replace("-","");
        }
        public DbSet<District> districts {get; set;}
        private static List<District> test_district = new List<District> 
        {
            new District{id = 2, name = "Железнодорожный"},
            new District{id = 3, name = "Кировский"},
            new District{id = 4, name = "Ленинский"},
            new District{id = 5, name = "Октябрьский"},
            new District{id = 6, name = "Свердловский"},
            new District{id = 7, name = "Советский"},
            new District{id = 8, name = "Центральный"},
            new District{id = 1, name = "Неопределенный"},
        };
        public DbSet<Ticket> tikets {get; set;}

        public DbSet<TrafficLight> traffic_lights {get; set;}
        private List<TrafficLight> test_light = new List<TrafficLight> 
        {
            new TrafficLight{id = 1, district_id = 1, hash_code = PostgreDataBase.hasher(-1 + ""), description = "Неопределенный"},
            new TrafficLight{id = 2, long_ = 42, lat = -42, district_id = 2, hash_code = PostgreDataBase.hasher(1 + "")},
            new TrafficLight{id = 3, long_ = 42, lat = -42, district_id = 3, hash_code = PostgreDataBase.hasher(2 + "")},
            new TrafficLight{id = 4, long_ = 42, lat = -42, district_id = 4, hash_code = PostgreDataBase.hasher(3 + "")},
            new TrafficLight{id = 5, long_ = 42, lat = -42, district_id = 5, hash_code = PostgreDataBase.hasher(4 + "")},
            new TrafficLight{id = 6, long_ = 42, lat = -42, district_id = 6, hash_code = PostgreDataBase.hasher(5 + "")},
            new TrafficLight{id = 7, long_ = 42, lat = -42, district_id = 7, hash_code = PostgreDataBase.hasher(6 + "")},
            new TrafficLight{id = 8, long_ = 42, lat = -42, district_id = 8, hash_code = PostgreDataBase.hasher(7 + "")},
            new TrafficLight{id = 9, long_ = 42, lat = -42, district_id = 2, hash_code = PostgreDataBase.hasher(8 + "")},
            new TrafficLight{id =10, long_ = 42, lat = -42, district_id = 3, hash_code = PostgreDataBase.hasher(9 + "")},
            new TrafficLight{id =11, long_ = 42, lat = -42, district_id = 4, hash_code = PostgreDataBase.hasher(10 + "")},
        };

        public DbSet<TicketState> ticket_states {get; set;}
        private List<TicketState> test_ticket_state = new List<TicketState> 
        {
            new TicketState{id = 1, name = "Поступила"},
            new TicketState{id = 2, name = "В обработке"},
            new TicketState{id = 3, name = "Выполнена"},
            new TicketState{id = 4, name = "Отменена"},
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
            new MobileUser{token = "testtoken1", surname = "Иванов1", name = "Иван1", father_name = "Иванович1", phone = "79535976614"},
            new MobileUser{token = "testtoken2", surname = "Иванов2", name = "Иван2", father_name = "Иванович2", phone = "79535976615"},
            new MobileUser{token = "testtoken3", surname = "Иванов3", name = "Иван3", father_name = "Иванович3", phone = "79535976616"},
            new MobileUser{token = "testtoken4", surname = "Иванов4", name = "Иван4", father_name = "Иванович4", phone = "79535976617"},
            new MobileUser{token = "testtoken5", surname = "Иванов5", name = "Иван5", father_name = "Иванович5", phone = "79535976618"},
        };

        public DbSet<TicketTrafficLight> ticket_traffic_lights {get; set;}

        public DbSet<TicketHistory> ticket_historys {get; set;}

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