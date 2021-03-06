using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdamFWatkins.Models
{
    public class Gallery
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string fullImage { get; set; }
        public int displayOrder { get; set; }
        public string Category { get; set; }
        public bool isDeleted { get; set; } 
    }
} 