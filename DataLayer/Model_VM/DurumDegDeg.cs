using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_VM
{
    public class DurumDegDeg
    {
        public string isim { get; set; }
        public bool maildog { get; set; }
        public bool usermi { get; set; }

        public bool yazarmi { get; set; }
        public bool adminmi { get; set; }

        public bool Doncek { get; set; }
    }
}
