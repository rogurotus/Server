using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("photo")]
    public class Photo
    {
        [Key]
        public int id {get; set;}

        public byte[] file {get; set;}

        public int ticket {get; set;}

    }
}