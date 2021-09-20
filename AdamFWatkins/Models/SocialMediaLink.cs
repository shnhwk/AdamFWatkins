using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AdamFWatkins.Models
{
    public class SocialMediaLink
    {
        [Key]
        public int id { get; set; } 
        [Editable(false)]
        [MaxLength(25)]
        public string key { get; set; }
        [Required] 
        public string link { get; set; }
    }
}