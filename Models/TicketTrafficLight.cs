using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("ticket_traffic_light")]
    public class TicketTrafficLight
    {
        [Key]
        public int ticket_id {get;set;} 
        public Ticket ticket {get; set;}

        public int traffic_light_id {get;set;} 
        [ForeignKey("traffic_light_id")]
        public TrafficLight traffic_light {get; set;}
    }
}