using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("ticket")]
    public class Ticket
    {
        [Key]
        public int id {get; set;}
        
        public string token {get; set;}

        public int state_id {get; set;} 
        [ForeignKey("state_id")]
        public TicketState state {get; set;}
    }
}