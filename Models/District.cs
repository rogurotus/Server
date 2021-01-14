using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("district")]
    public class District
    {
        [Key]
        public int id {get; set;}
        
        public string name {get; set;}
    }
}