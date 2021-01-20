using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("ticket_duplicate")]
    public class TicketDuplicate
    {
        public int main_tiket {get; set;}

        public int tiket {get; set;}
    }
}