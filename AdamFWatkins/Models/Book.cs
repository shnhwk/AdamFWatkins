using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdamFWatkins.Models
{
    public class Book
    { 
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string thumbNail { get; set; } 
        public string coverImage { get; set; } 
        public int displayOrder { get; set; }
        public string purchaseUrl { get; set; }
        public string purchaseUrl2 { get; set; }
        public string purchaseUrl3 { get; set; }
        public string purchaseUrl4 { get; set; }
        public string text { get; set; }
        public bool isDeleted { get; set; }
        public virtual ICollection<BookThumbs> bookThumbs { get; set; }
    } 
     
    public class BookThumbs 
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
        public string fullImage { get; set; }
        public int displayOrder { get; set; }
        public int bookId { get; set; }
        public bool isDeleted { get; set; }
    }
}
