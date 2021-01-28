using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("traffic_light")]
    public class TrafficLight
    {
        [Key]
        public int id {get; set;}

        public string hash_code {get; set;}

        [Column("long")]
        public double long_ {get; set;}

        public double lat {get; set;}

        public int district_id {get; set;}
        [ForeignKey("district_id")]
        public District district {get; set;}

        public string description {get; set;}
    }
}