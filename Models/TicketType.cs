using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("ticket_type")]
    public class TicketType
    {
        [Key]
        public int id {get; set;}

        public string name {get; set;}

        public string description {get; set;}

        public string url {get; set;}

    }
}