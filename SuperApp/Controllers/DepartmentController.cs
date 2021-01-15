using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P1_BuisnessLogicLayer;
using P1_ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperApp.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly BuisnessLogicClass _buisnessLogicClass;
        private readonly ILogger<ProductController> _logger;

        public DepartmentController(BuisnessLogicClass buisnessLogicClass, ILogger<ProductController> logger)
        {
            _buisnessLogicClass = buisnessLogicClass;
            _logger = logger;
        }

        // GET: DepartmentController
        [Authorize]
        public ActionResult Index()
        {
            List<Department> departments = _buisnessLogicClass.GetAllTheDepartments();

            return View(departments);
        }

        //////// GET: DepartmentController/Details/5
        //////public ActionResult Details(int id)
        //////{
        //////    return View();
        //////}

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _buisnessLogicClass.CreateNewDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
            
        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {

            Department department = _buisnessLogicClass.GetDepartmentByID(id);


            return View(department);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Department department)
        {
            if (ModelState.IsValid)
            {
                Department dbDepartment = _buisnessLogicClass.GetDepartmentByID(id);
                dbDepartment.Name = department.Name;
                _buisnessLogicClass.UpdateDepartment(dbDepartment);

                //Return to index...
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            Department department = _buisnessLogicClass.GetDepartmentByID(id);
            return View(department);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Department department)
        {
            _buisnessLogicClass.DeleteDepartment(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
