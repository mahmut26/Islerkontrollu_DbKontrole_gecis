using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_VM
{
    public class GuncellemeListe
    {
        public List<DurumDegDeg> Users { get; set; }
        public string SelectedUserEmail { get; set; }
        public string UpdateType { get; set; }
        public bool NewValue { get; set; }

    }
}
