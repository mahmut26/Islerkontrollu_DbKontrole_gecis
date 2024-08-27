using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_VM
{
    public class KullanicilarinDurumlari
    {
       public string UserName { get; set; }
             public bool EmailConfirmed { get; set; }
                public bool IsAdmin { get; set; }
                 public bool IsYazar { get; set; }
        public bool IsKullanici { get; set; }
    }

}

