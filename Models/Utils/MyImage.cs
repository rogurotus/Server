using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Server.Models
{
    [Table("image")]
    public class MyImage
    {
        [Key]
        public string name {get; set;}

        public byte[] file { get; set; }

    }
}