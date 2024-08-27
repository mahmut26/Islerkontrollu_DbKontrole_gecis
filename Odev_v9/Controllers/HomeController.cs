using Microsoft.AspNetCore.Mvc;
using Odev_v9.Models;
using System.Diagnostics;

namespace Odev_v9.Controllers
{
    public class HomeController : Controller //buraya da aslýnda þeyetmek lazým 1-2 gösterilecek ayar falan
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
