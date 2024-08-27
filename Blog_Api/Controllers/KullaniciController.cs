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
    
    public class KullaniciController : ControllerBase
    {
        private readonly Blog_DB _context;

        public KullaniciController(Blog_DB context)
        {
            _context = context;
        }
        


        /// <summary>
        /// Katagori ekliyor bu takip sistemli olarak düşününcene. Autorization gerekli tabiki
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        [HttpPost("kategori-ekle")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Kullanici")]
        public async Task<IActionResult> KategoriEkle(Link link)
        {
            
            Kullanici kullanici = _context.kullanicis.FirstOrDefault(x => x.Name == link.sorgu1); //mantıken bunun çalışmama imkanı yok ama olur mu olur kalsın 
            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı."); //api testi için yapmıştım 
            }

  
            var kategoriEntity = _context.kategoris.FirstOrDefault(x => x.Name == link.sorgu2); ////mantıken bunun çalışmama imkanı yok ama olur mu olur kalsın 
            if (kategoriEntity == null)
            {
                return NotFound("Kategori bulunamadı.");//api testi için yapmıştım . bunları zaten token ve sql den çekiyor. devamı diğer kontrollerde
            }

            kullanici.katid = kategoriEntity.Id; //gereksiz herhalde bu da yaptık bi kere o zaman mantıklı gelmişse demek


            if (kullanici.Kategoriler == null) //boş mu 
            {
                kullanici.Kategoriler = new List<Kategori>(); //yoksa yap
            }
            kullanici.Kategoriler.Add(kategoriEntity); //ekle

            await _context.SaveChangesAsync(); //kaydet

            return Ok("Kategori başarıyla eklendi.");
           

        }
        

        /// <summary>
        /// Bu da Takipteki başlıkları getiriyor. Öyle olmalı en azından uzun süre oldu bunu yapalı keşke önceden açıklama yazsaydım
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("baslik-goster")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Kullanici")]
        public async Task<IActionResult> BaslikGoster(Sorgu user)
        {
            var a = _context.kullanicis.Where(X => X.Name == user.Name); //ismi ara 
            
            var kategoriIds = await _context.kullanicis  //hatırlamıyorum
                        .Where(k => k.Name == user.Name)
                        .SelectMany(k => k.Kategoriler)
                        .Select(c => c.Id)
                        .ToListAsync();

            var makaleler = await _context.makales 
                .Where(m => kategoriIds.Contains(m.KategoriId))
                .ToListAsync();

            var baslik=makaleler.Select(x=> new {  //başlıkları getir
                x.Id,
                x.Baslik,
                }).ToList();

            List<Link_int2> aa =new List<Link_int2>();

            foreach (var item in baslik)
            {
                aa.Add(new Link_int2
                {
                    sorgu1 = item.Id,
                    sorgu2 = item.Baslik
                });
            }

            return Ok(aa);

           
        }

        /// <summary>
        /// Bu da Okumaya yarıyor. Bastın mesela hop isim döndü ismi db de arayıp döndürüyor. Basit olsun maksat o
        /// </summary>
        /// <param name="sorgu"></param>
        /// <returns></returns>
        [HttpPost("oku")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Kullanici")]
        public async Task<IActionResult> Oku(Sorgu sorgu)
        {
            
            var makale = await _context.makales.FirstOrDefaultAsync(x=>x.Baslik==sorgu.Name); //başlıktan getir 
            
            return Ok(makale);
        }
    }
}
