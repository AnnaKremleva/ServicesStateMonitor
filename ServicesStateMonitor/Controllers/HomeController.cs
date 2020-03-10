using Microsoft.AspNetCore.Mvc;
using ServicesStateMonitor.Models;
using System.Diagnostics;

namespace ServicesStateMonitor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
    }
}