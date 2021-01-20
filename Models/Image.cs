using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("image")]
    public class Image
    {
        [Key]
        public int id {get; set;}

        public byte[] file { get; set; }

    }
}