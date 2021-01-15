using Microsoft.AspNetCore.Authorization;
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
using System.Threading.Tasks;

namespace SuperApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController

        private readonly BuisnessLogicClass _buisnessLogicClass;
        private readonly ILogger<ProductController> _logger;
        //private readonly UserManager<Customer> _userManager;

        public ProductController(BuisnessLogicClass buisnessLogicClass, ILogger<ProductController> logger)
        {
            _buisnessLogicClass = buisnessLogicClass;
            _logger = logger;
            //_userManager = userManager;
        }

        [Authorize]
        public ActionResult Index()
        {
            //return a list of productViewModel
            List<ProductViewModel> Products = _buisnessLogicClass.GetAllTheProducts();

            //Convert the 

            return View(Products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Product myProduct = _buisnessLogicClass.GetProductByID(id);

            ProductViewModel myProductVM = _buisnessLogicClass.ConvertProductIntoVM(myProduct);

            return View(myProductVM);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {

            //Query the List of Departments available to the product...
            List<Department> departments = _buisnessLogicClass.GetAllTheDepartments();
            ViewBag.Departments = departments;

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel myProductVM, int DepartmentID)
        {
            if ( ModelState.IsValid)     
            {
                //Save the product to DB and display the product information...

                Department myDepartment = _buisnessLogicClass.GetDepartmentByID(DepartmentID);
                //Update Product
                Product myProduct = new Product()
                {
                    Name = myProductVM.Name,
                    Description = myProductVM.Description,
                    Price = myProductVM.Price,
                    Department = myDepartment
                };

                //Now we are going to save the brand new product and we are going to retrieve it with the id.

                ProductViewModel DbProductVM = _buisnessLogicClass.ConvertProductIntoVM( _buisnessLogicClass.CreateNewProduct(myProduct) );
                return View("Details", DbProductVM); 
            }
            else
            {
                // Idk...
                return BadRequest();
            }

            
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            
            Product myProduct = _buisnessLogicClass.GetProductByID(id);

            ProductViewModel productViewModel = _buisnessLogicClass.ConvertProductIntoVM(myProduct);

            //Query the List of Departments available to the product...
            List<Department> departments = _buisnessLogicClass.GetAllTheDepartments();
            ViewBag.Departments = departments;


            return View(productViewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]                                      ///Think it will work....
        public ActionResult Edit(int id, ProductViewModel myProductVM, int DepartmentID)
        {
            if ( ModelState.IsValid )
            {

                Department myDepartment = _buisnessLogicClass.GetDepartmentByID(DepartmentID);
                myProductVM.Department = myDepartment.Name;
                //Update Product
                Product myProduct = _buisnessLogicClass.GetProductByID(id);

                myProduct.Name = myProductVM.Name;
                myProduct.Description = myProductVM.Description;
                myProduct.Price = myProductVM.Price;
                myProduct.Department = myDepartment;
                //myProduct.Name = myProductVM.Name;


                _buisnessLogicClass.UpdateProduct(myProduct);

                return View( "Details", myProductVM);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            ProductViewModel myProductVM = _buisnessLogicClass.ConvertProductIntoVM( _buisnessLogicClass.GetProductByID(id) );
            return View(myProductVM);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductViewModel productViewModel)
        {

            _buisnessLogicClass.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
