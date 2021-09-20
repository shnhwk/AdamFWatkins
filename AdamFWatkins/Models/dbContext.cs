using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AdamFWatkins.Models
{
    public class dbContext : DbContext
    {
        public DbSet<Sketch> Sketches { get; set; }
        public DbSet<Illustration> Illustrations { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookThumbs> BookThumbs { get; set; }
        public DbSet<SiteText> SiteTexts { get; set; }
        public DbSet<SocialMediaLink> SocialMediaLinks { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
    } 
   
} 
 