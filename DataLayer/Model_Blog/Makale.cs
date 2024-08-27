using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataLayer.Model_Blog
{
    public class Makale
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        
        public string Icerik { get; set; }
        //public ICollection<Resim> resim { get; set; } //özellik bro

        //public Yorum yorum { get; set; } //özellik bro

        public int YazarId { get; set; }

        public Yazar yazar { get; set; }
        public int KategoriId { get; set; }
        public Kategori kategori { get; set; }


    }
}
