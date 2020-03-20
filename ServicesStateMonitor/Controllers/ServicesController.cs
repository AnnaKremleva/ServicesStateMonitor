using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;
using System.Text;

namespace ServicesStateMonitor.Controllers
{
    public class ServicesController : Controller
    {
        private const string NewLineMarker = "\r\n";
        private const string NewLineMarkerSingle = "\n";

        private readonly IServicesRepository _repository;

        public ServicesController(IServicesRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
            => View(_repository.Services);

        public ActionResult Create()
        {
            ViewBag.Dependencies = new MultiSelectList(_repository.GetDependencies(null));
            return View();
        }

        [HttpPost]
        public ActionResult Create(Service service)
        {
            try
            {
                ParseLinksText(service);
                _repository.Create(service);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Dependencies = new MultiSelectList(_repository.GetDependencies(null));
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            var service = _repository.GetById(id);
            if (service is null)
                return RedirectToAction(nameof(Index));

            UpdateLinksText(service);
            ViewBag.Dependencies = new MultiSelectList(_repository.GetDependencies(service));
            return View(service);
        }

        [HttpPost]
        public ActionResult Edit(Service service)
        {
            try
            {
                ParseLinksText(service);
                _repository.Update(service);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Dependencies = new MultiSelectList(_repository.GetDependencies(service));
                return View(service);
            }
        }

        public ActionResult Delete(string id)
        {
            var service = _repository.GetById(id);
            if (service is null)
                return RedirectToAction(nameof(Index));

            UpdateLinksText(service);
            ViewBag.Dependencies = new MultiSelectList(_repository.GetDependencies(service));
            return View(service);
        }

        [HttpPost]
        public ActionResult Delete(Service service)
        {
            try
            {
                _repository.Delete(service);
                ViewBag.Dependencies = new MultiSelectList(_repository.GetDependencies(service));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Dependencies = new MultiSelectList(_repository.GetDependencies(service));
                return View(service);
            }
        }

        //TODO this code is not for controller
        private void ParseLinksText(Service service)
        {
            service.EssentialLinks.Clear();
            if (!string.IsNullOrWhiteSpace(service.LinksText))
            {
                foreach (string link in service.LinksText.Split(NewLineMarker))
                {
                    if (!string.IsNullOrWhiteSpace(link))
                        service.EssentialLinks.Add(link);
                }
            }
        }

        private void UpdateLinksText(Service service)
        {
            var stringBuilder = new StringBuilder();
            foreach (string link in service.EssentialLinks)
            {
                stringBuilder.Append(link);
                stringBuilder.Append(NewLineMarkerSingle);
            }
            service.LinksText = stringBuilder.ToString();
        }
    }
}