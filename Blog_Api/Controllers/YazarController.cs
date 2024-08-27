using DataLayer.Model_Blog;
using DataLayer.Model_DBContext;
using DataLayer.Model_Kullanicilar;
using DataLayer.Model_VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class YazarController : ControllerBase
    {
        private readonly Blog_DB _context;

        public YazarController(Blog_DB context)
        {
            _context = context;
        }

        [HttpPost("basliklar")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Yazar")] //şunun için bir gün harcadım kitapsız 
        public async Task<IActionResult> yazilanlar(Sorgu yazar)
        {
             
            if(yazar == null) //nasıl null gelecekse artık. Eski koddan kaldı herhalde 
            {
                return BadRequest();
            }

            var yazarid= await _context.yazars.Where(y => y.Name == yazar.Name).Select(x => x.Id).ToListAsync(); // işlem yapıldı (evet okuyan da görebiliyor)
            int sorgu = yazarid[0];
            var makaleler = await _context.makales.Where(m => m.YazarId == sorgu).ToListAsync();
            var basliklar = _context.makales
                             .Where(m => m.YazarId == sorgu)
                             .Select(m => m.Baslik)
                             .ToList(); //yazılan başlıklar alındı 

            List<Sorgu> aa = new List<Sorgu>(); //bunun içinde sadece bir string olduğu için ve benim deserialize da hata çıkartmasından bıktığımdan viewmodeller çoğaldıydı
            foreach (var item in basliklar)
            {
                aa.Add(new Sorgu
                {
                    Name=item
                });
            }
            return Ok(aa);

           
        }

        [HttpPost("makale-ekle")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Yazar")]
        public async Task<IActionResult> MakaleYaz(MakaleViewModel model)
        {
            if (model.Baslik == null)// api testi bu 
            {
                return BadRequest();
            }

            var catid = await _context.kategoris.FirstOrDefaultAsync(k => k.Name == model.Cat); //yoksa oluştur - kategori

            if (catid == null)
            {
                catid = new Kategori
                {
                    Name = model.Cat,
                };

                _context.kategoris.Add(catid);
                await _context.SaveChangesAsync();
            }

            var yazid = await _context.yazars.FirstOrDefaultAsync(k => k.Name == model.Yaz); //yoksa oluştur -- yazar nasıl olacak bilmiyorum ama zamanında yapmıştım sonra gitti başka yerlere çaktırmayın :P

            if (yazid == null)
            {
                yazid = new Yazar
                {
                    Name = model.Yaz,
                };

                _context.yazars.Add(yazid);

                await _context.SaveChangesAsync();
            }

            var kadi = await _context.kategoris.Where(y => y.Name == model.Cat).Select(x => x.Id).ToListAsync(); //id getir

            var yadi = await _context.yazars.Where(y => y.Name == model.Yaz).Select(x => x.Id).ToListAsync(); //id getir

            Makale donus = new Makale()
            {
                Baslik=model.Baslik,
                Icerik = model.Icerik,
                KategoriId= kadi[0],
                YazarId = yadi[0],
            };

            Kategori a = _context.kategoris.FirstOrDefault(x=>x.Name==model.Cat);//oluşanı al 
             
            donus.kategori=a;

            Yazar b = _context.yazars.FirstOrDefault(x => x.Name == model.Yaz); //oluşanı al 

            donus.yazar = b;


            _context.makales.Add(donus);

            await _context.SaveChangesAsync();

            return Ok("Kaydoldu"); //yalan yok burası değişti az daha da ne yaptım hatırlamıyorum ara ara yazmak lazım böyle
             

        }
    }
}
