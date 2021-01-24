using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("ticket")]
    public class Ticket
    {
        [Key]
        public int id {get; set;}

        public int type_id {get; set;}

        [ForeignKey("type_id")]
        public TicketType type {get; set;}

        public int state_id {get; set;}
        [ForeignKey("state_id")]
        public TicketState state {get; set;}

        public string mobile_token {get; set;}

        [ForeignKey("mobile_token")]
        public MobileUser mobile_user {get; set;}

        public string description {get; set;}

        public string date_add {get; set;}

        public List<int> dublicates_id {get; set;}
    }
}