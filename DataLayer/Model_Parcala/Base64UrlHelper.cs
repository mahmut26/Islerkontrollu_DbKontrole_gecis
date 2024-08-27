using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model_Parcala
{
    public class Base64UrlHelper //internetten bulundu bu çalışıyor. Base 64 işlemi yapıp token i parçalıyor
    {
        public static string Base64UrlDecode(string base64Url)
        {
            // Base64 URL karakterlerini standart Base64 karakterlerine dönüştürün
            var base64 = base64Url.Replace('-', '+').Replace('_', '/');

            // Padding karakterlerini ekleyin
            var padding = base64.Length % 4;
            if (padding > 0)
            {
                base64 += new string('=', 4 - padding);
            }

            // Base64 formatını decode edin
            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
