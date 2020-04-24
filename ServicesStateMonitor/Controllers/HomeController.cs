using Microsoft.AspNetCore.Mvc;
using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;
using System.Diagnostics;

namespace ServicesStateMonitor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicesRepository _repository;

        public HomeController(IServicesRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            ViewBag.Connections = _repository.GetConnectionPairs();
            return View(_repository.Services);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
    }
}