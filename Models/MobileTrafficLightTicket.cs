using System.Collections.Generic;

namespace Server.Models
{
    public class MobileTrafficLightTicket
    {
        public int traffic_light_id {get; set;}
        public string user_token {get; set;}
        public string description {get;set;}
    }
}