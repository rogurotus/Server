using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("ticket_state")]
    public class TicketState
    {
        [Key]
        public int id {get; set;}

        public string name {get; set;}
    }
}