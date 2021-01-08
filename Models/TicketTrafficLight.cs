using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("ticket_traffic_light")]
    public class TicketTrafficLight
    {
        [Key]
        public int ticket_id {get; set;}
        [ForeignKey("tiket_id")]
        public Ticket ticket {get; set;}

        public TrafficLight traffic_light {get; set;}

        public string description {get; set;}
    }
}