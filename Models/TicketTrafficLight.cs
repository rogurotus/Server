using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("ticket_traffic_light")]
    public class TicketTrafficLight
    {
        [Key, ForeignKey("id")]
        public Ticket ticket {get; set;}

        [ForeignKey("id")]
        public TrafficLight traffic_light {get; set;}

        public string description {get; set;}
    }
}