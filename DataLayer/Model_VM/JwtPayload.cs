using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_VM
{
    public class JwtPayload
    {
        public string Sub { get; set; }
        public string IsUser { get; set; }
        public string IsYazar { get; set; }
        public string IsAdmin { get; set; }

        public string Name { get; set; }

    }
}
