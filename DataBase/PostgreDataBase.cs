using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.DataBase
{
    public class PostgreDataBase : DbContext
    {
        public DbSet<Ticket> tikets {get; set;}

        public DbSet<TrafficLight> traffic_lights {get; set;}

        public DbSet<TicketState> ticket_state {get; set;}

        public DbSet<TicketTrafficLight> ticket_traffic_lights {get; set;}
        public DbSet<OtherTicket> other_tickets {get; set;}
        
        public PostgreDataBase (DbContextOptions<PostgreDataBase> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}