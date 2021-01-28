using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("ticket_history")]
    public class TicketHistory
    {
        [Key]
        public int id {get; set;}
        public int ticket_id {get; set;}

        public string date {get; set;}

        public int ticket_state_old_id {get; set;}

        public TicketState ticket_state_old {get; set;}

        public int ticket_state_new_id {get; set;}
        public TicketState ticket_state_new {get; set;}

    }
}