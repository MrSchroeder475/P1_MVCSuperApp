using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P1_BuisnessLogicLayer;
using P1_ModelLib.Models;
using P1_ModelLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperApp.Controllers
{
    public class LocationController : Controller
    {

        private readonly BuisnessLogicClass _buisnessLogicClass;
        private readonly ILogger<ProductController> _logger;

        public LocationController(BuisnessLogicClass buisnessLogicClass, ILogger<ProductController> logger)
        {
            _buisnessLogicClass = buisnessLogicClass;
            _logger = logger;
        }
        // GET: LocationController
        [Authorize]
        public ActionResult Index()
        {
            List<LocationViewModel> locations = _buisnessLogicClass.GetAllTheLocation();

            return View(locations);
        }

        // GET: LocationController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Location myLocation = _buisnessLogicClass.GetLocationByID(id);

            LocationViewModel myLocationVM = _buisnessLogicClass.ConvertLocationIntoVM(myLocation);
            return View(myLocationVM);
        }

        // GET: LocationController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationViewModel locationViewModel)
        {
            if (ModelState.IsValid)
            {
                _buisnessLogicClass.CreateNewLocation(locationViewModel);

                return View("Details", locationViewModel);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: LocationController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {

            Location location = _buisnessLogicClass.GetLocationByID(id);

            LocationViewModel locationViewModel = _buisnessLogicClass.ConvertLocationIntoVM(location);

            return View(locationViewModel);
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationViewModel locationViewModel)
        {
            if (ModelState.IsValid)
            {
                _buisnessLogicClass.UpdateLocation( id, locationViewModel );

                return View("Details", locationViewModel);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: LocationController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Location location = _buisnessLogicClass.GetLocationByID(id);

            LocationViewModel locationViewModel = _buisnessLogicClass.ConvertLocationIntoVM(location);

            return View(locationViewModel);
        }

        // POST: LocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _buisnessLogicClass.DeleteLocation(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
