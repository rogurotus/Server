using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Server.Models
{
    public class TicketResponse
    {
        public int id {get; set;} //
        public TicketType type {get; set;} //
        public TicketState state {get; set;} //
        public MobileUser mobile_user {get; set;} //
        public District district {get; set;} //
        public string description {get; set;} //
        public string date_add {get; set;} //
        public List<int> dublicates_id {get; set;} //
        public double long_ {get; set;} //
        public double lat {get; set;} //
        public List<int> photo_id {get; set;} //
        public List<int> mini_photo_id {get; set;} //
        public List<TicketHistory> histories {get; set;} //
    }
}