using Azure;
using Blog_Api;
using DataLayer.Model_Blog;
using DataLayer.Model_DBContext;
using DataLayer.Model_Hatalar;
using DataLayer.Model_Parcala;
using DataLayer.Model_VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Odev_v9.Controllers
{
    public class YazarController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Blog_DB context;

        public YazarController(IHttpClientFactory httpClientFactory,Blog_DB _DB)
        {
            _httpClientFactory = httpClientFactory;
            context = _DB;
        }

        [HttpGet]
        public async Task<IActionResult> Makaleler() //klavyem kırık boşluk acıtıyor elimi
        {
            string token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string baseUrl = "https://localhost:7052/api/";
            string apiEndpoint = "Yazar/basliklar";
            string url = string.Concat(baseUrl, apiEndpoint);
            if (token == null)
            {
                ViewBag.Mesaj = "Token yok olm nereye gidiyorsun";
                return View("Ekle");
            }
            
            //parçala getir 

            string jsonPayload = Base64UrlHelper.Base64UrlDecode(token.Split('.')[1]);
            var payload = JsonConvert.DeserializeObject<JwtPayload>(jsonPayload);
            string yazarid = (payload.Name);
            

            // API'ye veri gönderme
            Sorgu sor = new Sorgu()
            {
                Name = yazarid
            };

            var payloaad = sor;
            
            
            var content = new StringContent(JsonConvert.SerializeObject(payloaad), Encoding.UTF8, "application/json");


            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();


                    var linkInt2List = JsonConvert.DeserializeObject<List<Sorgu>>(responseContent);


                    return View(linkInt2List);
                }
                else
                {
                    // API hatalarını işleme

                    ViewBag.Mesaj = $"{response.StatusCode}";
                    return View("Ekle");
                    
                }
            }
            catch (HttpRequestException ex)
            {
                // İstek hatalarını işleme
                return StatusCode(StatusCodes.Status500InternalServerError, $"Request error: {ex.Message}");
            }
            
        }

        [HttpGet("IOweMySoul")]
        public IActionResult MakaleyiYaz() //yazma viewi getir (token burda kullanılıyor olabilir hatırlamıyorum. Kullanmıyor galiba)
        {
            string token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return View();
        }


        [HttpPost] //api a gönderen yer burda
        public async Task<IActionResult> Yaz(MakaleViewModel model)
        {
            // Token'ı al
            string token = HttpContext.Session.GetString("Token");

            // Token yoksa hata döndür
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Mesaj = $"Token yok olm nereye gidiyorsun";
                return View("Ekle");
            }

           
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string baseUrl = "https://localhost:7052/api/";
            string apiEndpoint = "Yazar/makale-ekle";
            string url = $"{baseUrl}{apiEndpoint}";

            string jsonPayload = Base64UrlHelper.Base64UrlDecode(token.Split('.')[1]);
            var payload = JsonConvert.DeserializeObject<JwtPayload>(jsonPayload);
            string yazarid = payload.Name;
            model.Yaz=yazarid;

            // Model verilerini JSON formatına dönüştür
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            try
            {
                // API'ye POST isteği gönder
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Yanıtı kontrol et ve uygun şekilde işle
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    
                    ViewBag.Mesaj = responseContent;
                    return View("Ekle");
                }
                else
                {
                    // API hatalarını işleme
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }
            }
            catch (HttpRequestException ex)
            {
                // İstek hatalarını işleme
                return StatusCode(StatusCodes.Status500InternalServerError, $"İstek hatası: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Icerik(string sorgu) //Burada MVC döndürecek. MVC YOK !!! Yok çalışıyormuş bu yav. Üstten başlığın ordan tuş var basınca (Viewleri tamamen ben yapmadım) bunu getiriyor
        {
            string token = HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string baseUrl = "https://localhost:7052/api/";
            string metot = "Kullanici/oku";


            string url = string.Concat(baseUrl, metot);
            if (token == null)
            {

                ViewBag.Mesaj = "Token yok olm nereye gidiyorsun";
                return View("Ekle");
            }
            Sorgu gonder = new Sorgu()
            {
                Name = sorgu
            };

            var content = new StringContent(JsonConvert.SerializeObject(gonder), Encoding.UTF8, "application/json");



            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    var linkInt2List = JsonConvert.DeserializeObject<MakaleVM_v2>(responseContent);

                    var yazar = context.yazars.Find(linkInt2List.YazarId);
                    var kateg = context.kategoris.Find(linkInt2List.KategoriId);

                    MakaleViewModel doncek = new MakaleViewModel
                    {
                        Baslik = linkInt2List.Baslik,
                        Icerik = linkInt2List.Icerik,
                        Cat = kateg.Name,
                        Yaz = yazar.Name
                    };

                    return View(doncek);
                }
                else
                {
                    // API hatalarını işleme
                    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
            catch (HttpRequestException ex)
            {
                // İstek hatalarını işleme
                return StatusCode(StatusCodes.Status500InternalServerError, $"Request error: {ex.Message}");
            }

        }

    }
}




