using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("mobile_user")]
    public class MobileUser
    {
        [Key]
        public string token {get; set;}
        public string surname {get; set;}
        public string name {get; set;}

        public string father_name {get; set;}

        public string phone {get; set;}
    }
}