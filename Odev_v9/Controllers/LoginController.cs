using DataLayer.Model_Login;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using DataLayer.Model_VM;
using DataLayer.Model_Hatalar;
using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using DataLayer.Model_Parcala;
using DataLayer.Model_Blog;

namespace Odev_v9.Controllers
{
    public class LoginController : Controller //bak şimdi burası önem arz ediyor. Neden mi şundan ötürü "logine" bak burda
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View(); //register viewi 
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            string baseUrl = "https://localhost:7052/api/";
            string loginMethod = "Login/olustur";

            if (ModelState.IsValid)
            {
                var serializeUser = JsonConvert.SerializeObject(register);

                StringContent stringContent = new StringContent(serializeUser, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();

                string url = string.Concat(baseUrl, loginMethod);

                var result = client.PostAsync(url, stringContent).Result;

                string json = await result.Content.ReadAsStringAsync();

               
                if (result.Content.Headers.ContentType.MediaType == "application/json")
                {
                    
                    List<JsonHata> requirements = JsonConvert.DeserializeObject<List<JsonHata>>(json);
                    TempData["Requirements"] = JsonConvert.SerializeObject(requirements);

                    return RedirectToAction("DisplayRequirementsL"); //hata liste olursa böyle döncek 
                }
                else
                {
                    TempData["Requirements"] = json;

                    return RedirectToAction("DisplayRequirements"); //tekse böyle sırf bunun için uğraştımdı vaktinde (bi daha olsa bir daha uğraşırım)
                }
            }

            return View();
        }


        [HttpGet]
        public IActionResult Login() //üsttekini aynısı
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            string baseUrl = "https://localhost:7052/api/";
            string loginMethod = "Login/giris";

            if (ModelState.IsValid)
            {
                var serializeUser = JsonConvert.SerializeObject(login);

                StringContent stringContent = new StringContent(serializeUser, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();

                string url = string.Concat(baseUrl, loginMethod);

                var result = client.PostAsync(url, stringContent).Result;

                string json = await result.Content.ReadAsStringAsync();

                try
                {
                    Token token = JsonConvert.DeserializeObject<Token>(json);
                    
                    if (token != null)
                    {
                        HttpContext.Session.SetString("Token", token.token);
                        string tokken = HttpContext.Session.GetString("Token");
                        string jsonPayload = Base64UrlHelper.Base64UrlDecode(tokken.Split('.')[1]);
                        var payload = JsonConvert.DeserializeObject<JwtPayload>(jsonPayload);
                        string IsYazar = (payload.IsYazar);
                        string IsUser = (payload.IsUser);
                        string IsAdmin = (payload.IsAdmin);
                        if (IsUser == "true") //bunlar da aslında _layout için yapıldı
                        {
                            HttpContext.Session.SetString("User","Okur"); //çok okur
                            if (IsYazar == "true")
                            {
                                HttpContext.Session.SetString("Yazar", "Yazar,Çok yazar"); //Abi 2 milyar 250 milyoncuk - 2500 kilometre yol gittik abi
                                if (IsAdmin == "true") 
                                {
                                    HttpContext.Session.SetString("Admin", "Biz sizi banlatmadık kicklettik"); //Biz terbiyesizlik yapmadığımız halde bizi warrocktrden banlattınız 
                                }
                            }
                        }
                        return RedirectToAction("Success"); // Başarı durumunda yönlendirme
                    }
                }
                catch (JsonException ex)
                {
                    // Token deserialization hatası
                    if (result.Content.Headers.ContentType.MediaType == "application/json")
                    {
                        try
                        {
                            List<JsonHata> requirements = JsonConvert.DeserializeObject<List<JsonHata>>(json);
                            TempData["Requirements"] = JsonConvert.SerializeObject(requirements);
                            return RedirectToAction("DisplayRequirementsL");
                        }
                        catch (JsonException exe)
                        {
                            // JSON deserialization hatası işleme
                            TempData["Error"] = $"JSON deserialization error: {exe.Message}";
                            return RedirectToAction("Error"); // Hata sayfasına yönlendirme
                        }
                    }
                    else
                    {
                        TempData["Requirements"] = json;

                        return RedirectToAction("DisplayRequirements");
                    }
                    
                }

            }

            return View();
        }
        public async Task<IActionResult> Logout() //çıkış tarafı 
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token"); //token sil
            if (HttpContext.Session.GetString("User") != null) //üsttekileri tek tek sil
            {
                HttpContext.Session.Remove("User");

                if (HttpContext.Session.GetString("Yazar") != null)
                {
                    HttpContext.Session.Remove("Yazar");

                    if (HttpContext.Session.GetString("Admin") != null)
                    {
                        HttpContext.Session.Remove("Admin");
                    }
                }
            }
                                    
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DisplayRequirementsL() //buda maksat viewdi
        {
            // TempData'dan veriyi alma
            if (TempData["Requirements"] is string requirementsJson)
            {
                List<JsonHata> requirements = JsonConvert.DeserializeObject<List<JsonHata>>(requirementsJson);
                return View(requirements);
            }

            return View(new List<JsonHata>());
        }
        public IActionResult DisplayRequirements()//buda maksat viewdi
        {
            // TempData'dan veriyi alma
            if (TempData["Requirements"] is string requirementsJson)
            {
                
                ViewBag.ResponseContent = requirementsJson;
                
            }

            return View();
        }
        public IActionResult Error() //buda maksat viewdi
        {
            // Hata sayfasına yönlendirme
            if (TempData["Error"] is string errorMessage)
            {
                ViewBag.ErrorMessage = errorMessage;
            }

            return View();
        }
        public IActionResult Success() //buda maksat viewdi yok galiba bu da varsada vardır doğrudur.
        {
            // Başarı durumunu gösteren bir View
            return View();
        }
    }
}
