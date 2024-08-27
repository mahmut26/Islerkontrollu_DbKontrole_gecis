using DataLayer.Model_Blog;
using DataLayer.Model_DBContext;
using DataLayer.Model_Kullanicilar;
using DataLayer.Model_Login;
using DataLayer.Model_VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer", Policy = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly Identity_DB _DB;
        private readonly Blog_DB _ctxt;
        public AdminController(Identity_DB dB,Blog_DB b_db)
        {
            _DB = dB;
            _ctxt = b_db;
        }


        /// <summary>
        /// Şimdi burada bu şu işe yarıyordu ama artık yaramıyor Durum var artık
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpGet("KullaniciMi")]
        public async Task<IActionResult> KullaniciMi(string a)
        {

            var aa = _DB.Users.FirstOrDefault(x => x.Email == a); 

            var b = aa.IsKullanici;

            string msg = $"{aa} nın durumu {b}";

            return Ok(msg);
        }
        /// <summary>
        /// Kullanıcı yapıyordu da artık yapamıyor . Yapar da Admin MVC de yapmalıydı olmadı. Güncelleyecedim de yapmadım yalan yok
        /// </summary>
        /// <param name="durumDeg"></param>
        /// <returns></returns>
        [HttpGet("KullaniciYap")]
        public async Task<IActionResult> KullaniciYap(DurumDeg durumDeg)
        {
            string a = string.Empty;
            durumDeg.Name = a; //atandı burada
            
            var aa = _DB.Users.FirstOrDefault(x => x.Email == a); //git getir ismi
            aa.IsKullanici=durumDeg.Degistir; //bool geleni ata
            _DB.SaveChangesAsync(); //atananı kaydet

            var bb = aa.Email; //eski koddan kalmış herhalde bu ama yemi api kodu yazılacaktı

            var kontrol = _ctxt.kullanicis.FirstOrDefault(x=>x.Name==bb); //kullanıcı tablosuna bak bakim ben var mıyım kontrolü

            if (kontrol == null) //yoksam yap
            {
                Kullanici kullanici = new Kullanici()
                {
                    Name = bb
                };

                _ctxt.kullanicis.Add(kullanici);

                _ctxt.SaveChangesAsync();

                return Ok(aa);
            }
            else
            {
                return BadRequest("Var zaten sadece kullanilik claimi değişti !! - "); //açıklaması yapılmış zaten
            }
           
        }
        /// <summary>
        /// Bu önemli ama diğerlerini yaparken yapacam. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet("MailOrderOnay")]
        public async Task<IActionResult> MailOrderOnay(string a, bool b)
        {

            var aa = _DB.Users.FirstOrDefault(x => x.Email == a); //meyili bul getir - yada user i
            aa.EmailConfirmed = b; //tru yada fals yap
            _DB.SaveChangesAsync(); //kaydet

            return Ok(aa); //döndürmek gerek sonuçta ayıp (üsttekini kopyaladımdı)
        }
        /// <summary>
        /// Şimdi burada bu şu işe yarıyordu ama artık yaramıyor Durum var artık
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpGet("YazarMi")]
        public async Task<IActionResult> YazarMi(string a)
        {

            var aa = _DB.Users.FirstOrDefault(x => x.Email == a);

            var b = aa.IsYazar;

            string msg = $"{aa} nın durumu {b}";

            return Ok(msg);
        }
        /// <summary>
        /// Yazar yapıyor Isyazar varya he onu değiştiriyor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet("YazarYap")]
        public async Task<IActionResult> YazarYap(string a, bool b)
        {

            var aa = _DB.Users.FirstOrDefault(x => x.Email == a); //üstte useryap tı galiba onun eski hali bu işte aynısı aslında 2 şey eklemişim
            aa.IsYazar = b;
            _DB.SaveChangesAsync();

            var bb = aa.Email; var kontrol = _ctxt.kullanicis.FirstOrDefault(x => x.Name == bb);

            if (kontrol == null)
            {
                Yazar yazar = new Yazar()
                {
                    Name = bb
                };

                _ctxt.yazars.Add(yazar);

                _ctxt.SaveChangesAsync();

                return Ok(aa);
            }
            else
            {
                return BadRequest("Var zaten sadece yazarlik claimi değişti !! - ");
            }

           
        }
        /// <summary>
        /// Blog databasesine onaylama ekleyecem de daha var.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet("MakaleOnay")] //!!! Onay Yok Şu anda !!
        public async Task<IActionResult> MakaleOnay(string a, bool b)
        {

            var aa = _ctxt.makales.FirstOrDefault(x => x.Baslik == a); //onaylama yok çağırmıyorum zaten
            
            _DB.SaveChangesAsync();

            return Ok(aa);
        }


        /// <summary>
        ///Tüm durumu getiriyor. Çok ii bu bak
        /// </summary>
        /// <returns></returns>
        [HttpGet("Durum")] //!!! Onay Yok Şu anda !!
        public async Task<IActionResult> Durum() //seç getir
        {

            var aa = _DB.Users.Select(x => new
            {
                x.UserName,
                x.EmailConfirmed,
                x.IsAdmin,
                x.IsYazar,
                x.IsKullanici
            }).ToList(); //seçimlerim bu
            

            List<KullanicilarinDurumlari> kullanicilarinDurumlari = new List<KullanicilarinDurumlari>(); //belkide bir daha kullanmayacağım viewlerden temizlemek lazım aslında

            foreach (var user in aa) //listeye ekleme sistemim
            {

                kullanicilarinDurumlari.Add(new KullanicilarinDurumlari
                {
                    UserName = user.UserName,
                    EmailConfirmed = user.EmailConfirmed,
                    IsAdmin=user.IsAdmin,
                    IsKullanici=user.IsKullanici,
                    IsYazar=user.IsYazar
                });
            }

            return Ok(kullanicilarinDurumlari); //döndür listeyi
        }
        [HttpPost("SQLQUERY")]
       public IActionResult SQLQUERY(Link link)
        {
            string sorgu1 = link.sorgu1;
            string sorgu2 = link.sorgu2;
            string query = $@"UPDATE AspNetUsers SET {sorgu2} = CASE WHEN {sorgu2} = 1 THEN 0 ELSE 1 END WHERE UserName = @sorgu1;";

            try
            {
                
                    // ExecuteSqlRaw ile sorguyu çalıştırma
                    _DB.Database.ExecuteSqlRaw(query, new SqlParameter("@sorgu1", sorgu1));
                    return Ok("Query executed successfully.");
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Error executing query: {ex.Message}");
            }
        } 
    }
}
