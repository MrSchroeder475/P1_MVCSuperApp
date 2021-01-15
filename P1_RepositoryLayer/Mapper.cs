using Microsoft.Extensions.Logging;
using P1_ModelLib.Models;
using P1_ModelLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_RepositoryLayer
{
    public class Mapper
    {
        private readonly ILogger<Mapper> _logger;


        public Mapper(ILogger<Mapper> logger)
        {
            _logger = logger;
        }

        public ProductViewModel ConvertProductIntoProductVM(Product product)
        {
            ProductViewModel myViewModel = new ProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Department = product.Department.Name,
                ProductID = product.ProductID
            };

            return myViewModel;

        }
        public LocationViewModel ConvertLocationIntoLocationVM(Location location)
        {
            LocationViewModel locationViewModel = new LocationViewModel()
            {
                Name = location.Name,
                LocationID = location.LocationID,
                Address = location.Address
            };

            return locationViewModel;
        }

        public InventoryViewModel ConvertInventoryIntoInventoryVM(Inventory inventory)
        {
            InventoryViewModel inventoryViewModel = new InventoryViewModel()
            {
                InventoryID = inventory.InventoryID,
                ProductName = inventory.Product.Name,
                ProductPrice = inventory.Product.Price,
                ProductDescription = inventory.Product.Description,
                Quantity = inventory.Quantity,
            };

            return inventoryViewModel;
        }

        public CustomerViewModel ConvertCustomerIntoCustomerVM(Customer customer)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                LocationName = customer.Location.Name,
            };

            return customerViewModel;
        }
        public OrderViewModel ConvertOrderIntoOrderVM(Order order)
        {
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                CustomerID = order.Customer.Id,
                CustomerName = order.Customer.ToString(),
                LocationID = order.Location.LocationID,
                StoreName = order.Location.Name,
                Date = order.Date,
                TotalAmount = order.TotalAmount,
                isCartActive = order.IsCartActive,
                OrderID = order.OrderID
            };
            return orderViewModel;
        }

        public OrderDetailViewModel ConvertOrderDetailIntoOrderDetailVM(OrderDetail orderDetail)
        {
            OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel()
            {
                ProductName = orderDetail.Product.Name,
                ProductPrice = orderDetail.Product.Price,
                Quantity = orderDetail.Quantity,
                TotalAmountDetail = Math.Round( orderDetail.Product.Price * orderDetail.Quantity, 2 ),
            };
            return orderDetailViewModel;
        }

    }
}
