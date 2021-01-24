using Microsoft.AspNetCore.Http;

namespace Server.Models
{
    public class PhotoPostRequest
    {
        public int ticket_id {get; set;}
        public string user_token {get; set;}
        public IFormFile photo {get;set;}
    }
}