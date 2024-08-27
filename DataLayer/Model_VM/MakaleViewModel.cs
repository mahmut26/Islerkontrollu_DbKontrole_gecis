using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_VM
{
    public class MakaleViewModel
    {
        [Required]
        public string Baslik { get; set; }

        [Required]
        public string Icerik { get; set; }


        [Required]
        [Display(Name = "Kategori")]
        public string Cat { get; set; }

        [Required]
        [Display(Name = "Yazar")]
        public string Yaz { get; set; }
    }
}
