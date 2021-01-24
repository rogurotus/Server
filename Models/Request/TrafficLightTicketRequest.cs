using System.Collections.Generic;

namespace Server.Models
{
    public class TrafficLightTicketRequest
    {
        public string traffic_light_hash_code {get; set;}
        public string user_token {get; set;}
        public string description {get;set;}
    }
}