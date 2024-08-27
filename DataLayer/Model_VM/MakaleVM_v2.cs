using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_VM
{
    public class MakaleVM_v2
    {
        public string Baslik { get; set; }

        //[Required]
        public string Icerik { get; set; }


        //[Required]
        //[Display(Name = "Kategori")]
        public int YazarId { get; set; }

        //[Required]
        //[Display(Name = "Yazar")]
        public int KategoriId { get; set; }
    }
}
