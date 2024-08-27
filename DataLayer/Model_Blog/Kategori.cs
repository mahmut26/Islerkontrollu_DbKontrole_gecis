using DataLayer.Model_Kullanicilar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model_Blog
{
    public class Kategori
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Makale> makales { get; set; }

        //public ICollection<Kullanici> Kullanicilar { get; set; } //iptal mı etmişim acaba bunu ne yaptımdı
    }
}