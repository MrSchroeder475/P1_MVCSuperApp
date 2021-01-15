using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P1_BuisnessLogicLayer;
using P1_ModelLib.Models;
using P1_ModelLib.ViewModels;
using System;
using System.Collections.Generic;

namespace SuperApp.Controllers
{
    public class OrderController : Controller
    {

        private readonly BuisnessLogicClass _buisnessLogicClass;
        private readonly ILogger<ProductController> _logger;

        public OrderController(BuisnessLogicClass buisnessLogicClass, ILogger<ProductController> logger)
        {
            _buisnessLogicClass = buisnessLogicClass;
            _logger = logger;
        }

        // GET: OrderController
        [Authorize]
        public ActionResult Index()
        {

            string UserEmail = User.Identity.Name;
            //Get the customer Store Name:
            Customer customer = _buisnessLogicClass.GetLoggedUserByUserName(UserEmail);

            HttpContext.Session.SetInt32("StoreID", customer.Location.LocationID);
            HttpContext.Session.SetInt32("UserID",customer.Id);

            ViewBag.storeLocation = customer.Location.Name;

            return View();
        }

        /*The menu will be :
        
        Generate order
        View the customer carts
        View all the store Orders

        */
        

        // GET: OrderController/CreateCart
        [Authorize]
        public ActionResult CreateCart()
        {
            int StoreID = (int) HttpContext.Session.GetInt32("StoreID");
            int UserID = (int)HttpContext.Session.GetInt32("UserID");

            //Verify if it doesnt exist an pending Order, returns the OrderID created or existing:
            int OrderID = _buisnessLogicClass.CreateOrUpdatePendingOrder(UserID, StoreID);
            HttpContext.Session.SetInt32("OrderID", OrderID);

            //Get all the Inventory from the store.
            List<InventoryViewModel> inventories = _buisnessLogicClass.GetAllTheInventoryFromStore(StoreID);

            return View(inventories);
        }

        // POST: OrderController/FinishOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishOrder(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        // GET: OrderController/AddQuantity/5
        public ActionResult AddQuantity(int Inventoryid)
        {
            int StoreID = (int)HttpContext.Session.GetInt32("StoreID");

            Inventory inventory = _buisnessLogicClass.GetInventoryFromStoreByID(Inventoryid, StoreID);
            InventoryViewModel inventoryViewModel = _buisnessLogicClass.ConvertInventoryIntoVM(inventory);  

            return View(inventoryViewModel);
        }

        // POST: OrderController/AddQuantity/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuantity(InventoryViewModel inventoryViewModel)
        {
            int StoreID = (int)HttpContext.Session.GetInt32("StoreID");
            int UserID = (int)HttpContext.Session.GetInt32("UserID");

            try
            {
                //Decrease the quantity in Inventory and Update the product Order
                _buisnessLogicClass.SetQuantityForOrder(inventoryViewModel, StoreID, UserID);
                return RedirectToAction(nameof(CreateCart));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Failure", ex.Message);
                return RedirectToAction(nameof(CreateCart));
            }
        }

        //GET: OrderController/ListCustomerOrders
        [Authorize]
        public ActionResult ListCustomerOrders()
        {
            int UserID = (int)HttpContext.Session.GetInt32("UserID");

            List<OrderViewModel> orderViewModel = _buisnessLogicClass.GetAllTheOrdersByLoggedCustomer(UserID);


            return View(orderViewModel);
        }

        [Authorize]
        public ActionResult ListStoreOrders()
        {
            int StoreID = (int)HttpContext.Session.GetInt32("StoreID");

            List<OrderViewModel> orderViewModels = _buisnessLogicClass.GetAllTheOrdersByCurrentLocation(StoreID);

            return View(orderViewModels);
        }
        
        [Authorize]
        public ActionResult FinishOrder()
        {
            int OrderID = (int)HttpContext.Session.GetInt32("OrderID");

            //Complete the order by marking that isCartActive to false.

            _buisnessLogicClass.CompleteOrder(OrderID);

            //return to the customer List of orders fullfilled.
            return RedirectToAction(nameof( ListCustomerOrders ));

        }
        [Authorize]
        public ActionResult ListOrderDetails(int OrderID)
        {

            List<OrderDetailViewModel> orderDetailViewModels = _buisnessLogicClass.GetAllTheOrderDetailByOrderID(OrderID);

            return View(orderDetailViewModels);
        }


    }
}
