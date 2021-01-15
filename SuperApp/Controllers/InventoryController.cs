using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P1_BuisnessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P1_ModelLib.ViewModels;
using Microsoft.AspNetCore.Authorization;
using P1_ModelLib.Models;
using System.Web;

namespace SuperApp.Controllers
{
    public class InventoryController : Controller
    {


        private readonly BuisnessLogicClass _buisnessLogicClass;
        private readonly ILogger<ProductController> _logger;

        public InventoryController(BuisnessLogicClass buisnessLogicClass, ILogger<ProductController> logger)
        {
            _buisnessLogicClass = buisnessLogicClass;
            _logger = logger;
        }


        // GET: InventoryController
        [Authorize]
        public ActionResult Index(int storeId, string storeName)
        {
            ViewBag.StoreName = storeName;
            ViewBag.StoreID = storeId;

            HttpContext.Session.SetString("StoreID", storeId.ToString());
            HttpContext.Session.SetString("storeName", storeId.ToString());

            List<InventoryViewModel> inventoryViewModels = _buisnessLogicClass.GetAllTheInventoryFromStore(storeId);

            return View(inventoryViewModels);
        }

        [Authorize]
        public ActionResult GetListInentory()
        {


            int StoreID = 0;
            string StoreName = "";

            int.TryParse(HttpContext.Session.GetString("StoreID"),out StoreID);
            StoreName = HttpContext.Session.GetString("storeName");

            List<InventoryViewModel> inventoryViewModels = _buisnessLogicClass.GetAllTheInventoryFromStore(StoreID);

            return View("Index", inventoryViewModels);
        }

        [Authorize]
        // GET: InventoryController/Details/5
        public ActionResult Details(int id)
        {
            int StoreID = 0;

            int.TryParse(HttpContext.Session.GetString("StoreID"), out StoreID);

            Inventory inventory = _buisnessLogicClass.GetInventoryFromStoreByID(id, StoreID);

            InventoryViewModel inventoryViewModel = _buisnessLogicClass.ConvertInventoryIntoVM( inventory );
            return View(inventoryViewModel);
        }

        [Authorize]
        // GET: InventoryController/Create
        public ActionResult Create()
        {
            //Load the list of Products.
            List<ProductViewModel> products = GetProductViewModelsForDropDown();
            ViewBag.Products = products;

            return View();
        }

        // POST: InventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryViewModel inventoryViewModel, int ProductID)
        {
            if (ModelState.IsValid)
            {
                int StoreID = 0;

                int.TryParse(HttpContext.Session.GetString("StoreID"), out StoreID);

                try
                {
                    Inventory inventory = _buisnessLogicClass.CreateNewInventory(inventoryViewModel, ProductID, StoreID);
                    inventoryViewModel = _buisnessLogicClass.ConvertInventoryIntoVM(inventory);
                    return View("Details", inventoryViewModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    ModelState.AddModelError("Failure", ex.Message);

                    List<InventoryViewModel> inventoryViewModels = _buisnessLogicClass.GetAllTheInventoryFromStore(StoreID);    

                    return View("Index", inventoryViewModels);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: InventoryController/Edit/5
        ///-------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult Edit(int id)
        {

            int StoreID = 0;

            int.TryParse(HttpContext.Session.GetString("StoreID"), out StoreID);

            Inventory inventory = _buisnessLogicClass.GetInventoryFromStoreByID(id, StoreID);
            InventoryViewModel inventoryViewModel = _buisnessLogicClass.ConvertInventoryIntoVM(inventory);

            List<ProductViewModel> products = GetProductViewModelsForDropDown();
            ViewBag.Products = products;

            return View(inventoryViewModel);
        }
            
        // POST: InventoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InventoryViewModel inventoryViewModel)
        {
            int StoreID = 0;

            int.TryParse(HttpContext.Session.GetString("StoreID"), out StoreID);

            //Instead of reviewing if the model is valid, we only update the Quantity.
            _buisnessLogicClass.UpdateInventoryQuantity(id, StoreID, inventoryViewModel.Quantity);

            List<InventoryViewModel> inventoryViewModels = _buisnessLogicClass.GetAllTheInventoryFromStore(StoreID);

            return View("Index", inventoryViewModels);
        }

        // GET: InventoryController/Delete/5
        public ActionResult Delete(int id)
        {
            int StoreID = 0;

            int.TryParse(HttpContext.Session.GetString("StoreID"), out StoreID);

            Inventory inventory = _buisnessLogicClass.GetInventoryFromStoreByID(id, StoreID);
            InventoryViewModel inventoryViewModel = _buisnessLogicClass.ConvertInventoryIntoVM(inventory);

            return View(inventoryViewModel);
        }

        // POST: InventoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, InventoryViewModel inventoryViewModel)
        {
            int StoreID = 0;

            int.TryParse(HttpContext.Session.GetString("StoreID"), out StoreID);

            _buisnessLogicClass.DeleteInventory(id, StoreID);

            List<InventoryViewModel> inventoryViewModels = _buisnessLogicClass.GetAllTheInventoryFromStore(StoreID);

            return View("Index", inventoryViewModels);
        }

        internal List<ProductViewModel> GetProductViewModelsForDropDown()
        {
            List<ProductViewModel> products = _buisnessLogicClass.GetAllTheProducts();
            return products;
        }
    }
}
