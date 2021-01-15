using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("web_user")]
    public class WebUser
    {
        [Key]
        public string login {get; set;}

        public string pass {get; set;}

        public string email {get; set;}
    }
}