using DataLayer.Model_Blog;
using DataLayer.Model_Kullanicilar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_DBContext
{

    public class Blog_DB : DbContext
    {
        public Blog_DB(DbContextOptions<Blog_DB> options) : base(options)
        {
        }
        //public DbSet<Etiket> etikets { get; set; } //özellik bro
        public DbSet<Kategori> kategoris { get; set; }
        public DbSet<Kullanici> kullanicis { get; set; }
        //public DbSet<MakaleEtiket> makaleEtikets { get; set; } //özellik bro
        public DbSet<Makale> makales { get; set; }
        public DbSet<Resim> resims { get; set; }
        public DbSet<Yazar> yazars { get; set; }
        //public DbSet<Yorum> yorums { get; set; } //özellik bro
    }
}
