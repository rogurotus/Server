using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("other_ticket")]
    public class OtherTicket
    {
        [Key, ForeignKey("id")]
        public Ticket ticket {get; set;}

        public string problem {get; set;}

        public string description {get; set;}
    }
}