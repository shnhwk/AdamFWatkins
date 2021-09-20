using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdamFWatkins.Models
{
    public class Illustration
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
        public string fullImage { get; set; }
        public int displayOrder { get; set; }
        public bool isDeleted { get; set; }
    }
}