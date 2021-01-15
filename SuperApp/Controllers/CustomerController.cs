using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P1_BuisnessLogicLayer;
using P1_ModelLib.Models;
using P1_ModelLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperApp.Controllers
{
    public class CustomerController : Controller
    {

        private readonly BuisnessLogicClass _buisnessLogicClass;
        private readonly ILogger<ProductController> _logger;
        private readonly UserManager<Customer> _userManager;

        public CustomerController(BuisnessLogicClass buisnessLogicClass, ILogger<ProductController> logger, UserManager<Customer> userManager)
        {
            _buisnessLogicClass = buisnessLogicClass;
            _logger = logger;
            _userManager = userManager;
        }


        // GET: CustomerController
        public ActionResult Index()
        {
            List<Customer> customers = _buisnessLogicClass.GetAllTheCustomers();
            List<CustomerViewModel> customerViewModels = new List<CustomerViewModel>();

            foreach (Customer c in customers)
            {
                customerViewModels.Add( _buisnessLogicClass.ConvertCustomerIntoVM( c ) );
            }

            return View(customerViewModels);
        }

        //// GET: CustomerController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: CustomerController/Create
        //public ActionResult Create()
        //{

           
        //    return View();
        //}

        //// POST: CustomerController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            CustomerViewModel customerViewModel = _buisnessLogicClass.GetCustomerVMByID(id);


            List<LocationViewModel> Locations = _buisnessLogicClass.GetAllTheLocation();
            ViewBag.Locations = Locations;

            return View(customerViewModel);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerViewModel customerViewModel, int LocationID)
        {

            _buisnessLogicClass.UpdateCustomer(id, customerViewModel, LocationID);
            

            List<Customer> customers = _buisnessLogicClass.GetAllTheCustomers();

            List<CustomerViewModel> customerViewModels = new List<CustomerViewModel>();

            foreach (Customer c in customers)
            {
                customerViewModels.Add(_buisnessLogicClass.ConvertCustomerIntoVM(c));
            }

            return View("Index",customerViewModels);
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            CustomerViewModel customerViewModel = _buisnessLogicClass.GetCustomerVMByID(id);

            return View(customerViewModel);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CustomerViewModel customerViewModel)
        {
            try
            {
                //Email
                string UserEmail = User.Identity.Name;

                _buisnessLogicClass.DeleteUser(id, UserEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Failure", ex.Message);

            }

            List<Customer> customers = _buisnessLogicClass.GetAllTheCustomers();
            List<CustomerViewModel> customerViewModels = new List<CustomerViewModel>();

            foreach (Customer c in customers)
            {
                customerViewModels.Add(_buisnessLogicClass.ConvertCustomerIntoVM(c));
            }

            return View("Index", customerViewModels);
        }
    }
}
