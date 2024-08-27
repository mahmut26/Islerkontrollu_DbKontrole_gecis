using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_Hatalar
{
    public class JsonHata
    {
        public string Code { get; set; }
        [AllowNull]
        public string Description { get; set; }
    }
}
