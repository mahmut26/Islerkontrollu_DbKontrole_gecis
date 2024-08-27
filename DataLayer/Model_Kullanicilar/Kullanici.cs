using DataLayer.Model_Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_Kullanicilar
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; } //özellik bro

        public int katid { get; set; }

        public ICollection<Kategori> Kategoriler { get; set; }


    } 
}
