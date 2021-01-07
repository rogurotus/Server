using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("traffic_light")]
    public class TrafficLight
    {
        [Key]
        public int id {get; set;}

        [Column("long")]
        public float long_ {get; set;}

        public float lat {get; set;}
    }
}