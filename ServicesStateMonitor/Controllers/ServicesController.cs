using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;

namespace ServicesStateMonitor.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServicesRepository _repository;

        public ServicesController(IServicesRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(_repository.Services);
        }

        public ActionResult Details(string id)
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Services = new MultiSelectList(_repository.Services);
            return View();
        }

        [HttpPost]
        public ActionResult Create(Service service)
        {
            try
            {
                _repository.Create(service);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Create update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Create delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}