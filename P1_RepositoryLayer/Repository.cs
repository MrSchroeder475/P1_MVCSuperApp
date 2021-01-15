using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P1_ModelLib.Models;
using P1_ModelLib.ViewModels;
using Store_RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_RepositoryLayer
{
    public class Repository
    {   
        private readonly ILogger<Repository> _logger;

        private readonly StoreDbContext _storeDbContext;
        private readonly Mapper _mapper;

        /// <summary>
        /// Constructor for our Repository Layer Object. It initializes the Lists and gets the context from any production or in-memory DB
        /// </summary>
        public Repository(StoreDbContext dbContextClass, ILogger<Repository> logger, Mapper mapper)
        {

            _storeDbContext = dbContextClass;
            DbInitializer.Initialize(_storeDbContext);
            //this.Locations = _storeDbContext.Locations.ToList();
            //this.Products = _storeDbContext.Products.ToList();
            //this.Inventory = _storeDbContext.Inventory.ToList();
            //this.Orders = _storeDbContext.Orders.ToList();
            _logger = logger;
            _mapper = mapper;
        }


        //internal List<Location> Locations = new List<Location>();
        //internal List<Product> Products = new List<Product>();
        //internal List<Inventory> Inventory = new List<Inventory>();
        //internal List<Order> Orders = new List<Order>();

        public Location GetDefaultLocationForRegisterCustomer()
        {
            Location myLocation = _storeDbContext.Locations.FirstOrDefault(x => x.Name == "Central Branch");

            return myLocation;
        }

        public List<ProductViewModel> GetAllTheProducts()
        {
            List<Product> Products = _storeDbContext.Products.Include( product => product.Department ). ToList();

            List<ProductViewModel> listProductVM = new List<ProductViewModel>();

            foreach (Product prod in Products)
            {
                listProductVM.Add(_mapper.ConvertProductIntoProductVM(prod));
            }


            return listProductVM;
        }

        internal void SaveChangesToDb()
        {
            _storeDbContext.SaveChanges();
        }

        public Product CreateNewProduct(Product myProduct)
        {
            _storeDbContext.Products.Add(myProduct);
            SaveChangesToDb();

            Product DbProduct = _storeDbContext.Products.First(x => x.Name == myProduct.Name && x.Description == myProduct.Description && x.Price == myProduct.Price);

            return DbProduct;
        }

        public void UpdateProduct(Product myProduct)
        {
            _storeDbContext.Products.Update(myProduct);
            SaveChangesToDb();

        }

        public void DeleteProduct(int id)
        {
            Product myProduct = _storeDbContext.Products.First(x => x.ProductID == id);
            _storeDbContext.Remove(myProduct);
            SaveChangesToDb();
        }

        public Product GetProductByID(int id)
        {
            Product myProduct = _storeDbContext.Products.Include(product => product.Department ).FirstOrDefault(x => x.ProductID == id);
            return myProduct;
        }

        public ProductViewModel ConvertProductIntoVM(Product product)
        {
            ProductViewModel productViewModel = _mapper.ConvertProductIntoProductVM(product);

            return productViewModel;
        }

        public List<Inventory> GetAllTheInventoryFromStore(int storeID)
        {
            Location StoreInventory = GetStoreLocationIncludingInventoryAndProduct(storeID);

            return StoreInventory.Inventory;
        }

        public InventoryViewModel ConvertInventoryIntoVM(Inventory inventory)
        {
            InventoryViewModel inventoryViewModel = _mapper.ConvertInventoryIntoInventoryVM(inventory);
            return inventoryViewModel;
        }


        public List<Department> GetAllTheDepartments()
        {
            List<Department> departments = _storeDbContext.Departments.ToList();
            return departments;
        }

        public Department GetDepartmentByID(int id)
        {
            Department department = _storeDbContext.Departments.FirstOrDefault(x => x.DepartmentID == id);
            return department;
        }

        public void CreateNewDepartment(Department department)
        {
            _storeDbContext.Departments.Add(department);
            SaveChangesToDb();
        }

        public void UpdateDepartment(Department department)
        {
            _storeDbContext.Departments.Update(department);
            SaveChangesToDb();
        }

        public void DeleteDepartment(int id)
        {
            Department department = _storeDbContext.Departments.First(x => x.DepartmentID == id);
            _storeDbContext.Remove(department);
            SaveChangesToDb();
        }

        public List<LocationViewModel> GetAllTheLocation()
        {
            List<Location> locations = _storeDbContext.Locations.ToList();

            //Mapper
            List<LocationViewModel> locationViewModel = new List<LocationViewModel>();

            foreach (Location loc in locations)
            {
                locationViewModel.Add(_mapper.ConvertLocationIntoLocationVM(loc)); 
            }

            return locationViewModel;
        }

        public Location GetLocationByID(int id)
        {
            Location myLocation = _storeDbContext.Locations.FirstOrDefault(x => x.LocationID == id);

            return myLocation;
        }

        public LocationViewModel ConvertLocationIntoVM(Location location)
        {

            LocationViewModel locationViewModel = _mapper.ConvertLocationIntoLocationVM(location);
            return locationViewModel;
        }

        public void CreateNewLocation(LocationViewModel locationViewModel)
        {
            Location myLocation = new Location();

            myLocation.Name = locationViewModel.Name;
            myLocation.Address = locationViewModel.Address;

            _storeDbContext.Locations.Add(myLocation);

            SaveChangesToDb();
        }

        public void UpdateLocation(Location location)
        {
            _storeDbContext.Locations.Update(location);
            SaveChangesToDb();
        }

        public void DeleteLocation(Location location)
        {
            _storeDbContext.Locations.Remove(location);
            SaveChangesToDb();
        }

        public Inventory CreateNewInventory(InventoryViewModel inventoryViewModel, int ProductID,int StoreID)
        {
            Location StoreInventory = GetStoreLocationIncludingInventoryAndProduct(StoreID);

            // Verify if its exist 
            if ( StoreInventory.Inventory.Exists( x => x.Product.ProductID == ProductID) )
            {
                //We will add the product
                //Validate if it it in the limit..
                Inventory inventory = StoreInventory.Inventory.First(x => x.Product.ProductID == ProductID);


                if ( inventory.Quantity + inventoryViewModel.Quantity > 100 )
                {
                    //It is the defined limit, throw a exception
                    throw new Exception("Error while updating the product. The Product limit is 99.");
                }
                else
                {
                    // Proceed to update the product
                    StoreInventory.Inventory.First(x => x.Product.ProductID == ProductID).Quantity += inventoryViewModel.Quantity;

                    UpdateLocation(StoreInventory);

                }
            }
            else
            {
                //Is a new inventory
                Inventory inventory = new Inventory()
                {
                    Product = this.GetProductByID(ProductID),
                    Quantity = inventoryViewModel.Quantity
                };

                StoreInventory.Inventory.Add(inventory);

                _storeDbContext.Locations.Update(StoreInventory);

                SaveChangesToDb();
            }


            Inventory returnInv = GetStoreLocationIncludingInventoryAndProduct(StoreID)
                .Inventory
                .FirstOrDefault(x => x.Product.ProductID == ProductID);

            return returnInv;


        }

        public void UpdateInventory(Inventory inventory)
        {
            _storeDbContext.Inventory.Update(inventory);
            SaveChangesToDb();
            
        }

        private Location GetStoreLocationIncludingInventoryAndProduct(int StoreID)
        {
            Location StoreInventory = _storeDbContext.Locations
              .Include(store => store.Inventory)
              .ThenInclude(inventory => inventory.Product)
              .First(x => x.LocationID == StoreID);
            return StoreInventory;
        }

        public void UpdateInventoryQuantity(int id, int StoreID, int Quantity)
        {
            Location StoreLocation = GetStoreLocationIncludingInventoryAndProduct(StoreID);

            //Change the inventory in the location
            StoreLocation.Inventory.First(x => x.InventoryID == id).Quantity = Quantity;

            _storeDbContext.Locations.Update(StoreLocation);
            SaveChangesToDb();

        }

        public void DeleteInventory(int id, int StoreID)
        {
            Location StoreLocation = GetStoreLocationIncludingInventoryAndProduct(StoreID);

            Inventory inventory = StoreLocation.Inventory.First(x => x.InventoryID == id);

            StoreLocation.Inventory.Remove(inventory);

            _storeDbContext.Locations.Update(StoreLocation);
            SaveChangesToDb();
        }

        public List<Customer> GetAllTheCustomers()
        {
            List<Customer> customers = _storeDbContext.Users
                .Include(customer => customer.Location).ToList();
            return customers;
        }

        public CustomerViewModel ConvertCustomerIntoVM(Customer customer)
        {
            CustomerViewModel customerViewModel = _mapper.ConvertCustomerIntoCustomerVM(customer);
            return customerViewModel;   
        }

        public Customer GetCustomerByID(int id)
        {
            Customer customer = _storeDbContext.Users
                .Include(customer => customer.Location)
                .First(x => x.Id == id);
            return customer;
        }

        public void UpdateCustomer(int id,CustomerViewModel customerViewModel, int LocationID)
        {
            Customer customer = _storeDbContext.Users.First( x => x.Id == id );

            Location location = GetStoreLocationIncludingInventoryAndProduct(LocationID);

            customer.FirstName = customerViewModel.FirstName;
            customer.LastName = customerViewModel.LastName;
            customer.Location = location;

            _storeDbContext.Users.Update(customer);
            // Review This method....
            SaveChangesToDb();
            
        }

        public void DeleteUser(int id, string UserEmail)
        {
            //Verify if is the same user, if so, cancel request in a exception
            Customer customer = _storeDbContext.Users.First(x => x.Id == id);

            if (customer.Email == UserEmail)
            {
                //Means it tries to delete himself.
                throw new Exception("The user can't delete himself.");
            }
            
                //Delete that user.
                _storeDbContext.Users.Remove(customer);
                SaveChangesToDb();
        }

        public Customer GetLoggedUserByUserName(string UserEmail)
        {
            Customer customer = _storeDbContext.Users
                .Include(user => user.Location)
                .First(x => x.UserName == UserEmail);
            return customer;
        }
        public int CreateOrUpdatePendingOrder(int UserID, int StoreID)
        {
            //Verify if it exists a pending order in the table for the user and the store he actual is.
            Order order = GetOrderByUserIDOrderIDAndIsActive(UserID, StoreID);

            int OrderID = 0;

            if (order == null)
            {
                //It doesnt exists, create a new order.
                Order NewOrder = new Order()
                {
                    Customer = GetCustomerByID(UserID),
                    IsCartActive = true,
                    Location = GetLocationByID(StoreID),
                    TotalAmount = 0,
                    Date = DateTime.Now
                };

                _storeDbContext.Orders.Add(NewOrder);
                SaveChangesToDb();

                OrderID = _storeDbContext.Orders.First(x => x.Customer.Id == UserID && x.Location.LocationID == StoreID && x.IsCartActive == true).OrderID;
            }
            else 
            {
                //else it exists, we return the OrderID for saving the OrderDetails
                OrderID = order.OrderID;

            }
            return OrderID;
        }

        public Order GetOrderByUserIDOrderIDAndIsActive(int UserID, int StoreID)
        {
            Order order = _storeDbContext.Orders
                .Include(order => order.OrderDetails)
                .ThenInclude(odetail => odetail.Product)
                .FirstOrDefault(x => x.Customer.Id == UserID && x.Location.LocationID == StoreID && x.IsCartActive == true);
            return order;
        }


        public void SetQuantityForOrder(InventoryViewModel inventoryViewModel, int StoreID, int UserID)
        {
            //First Validate if the Quantity do not pass the actual quantity
            Inventory inventory = _storeDbContext.Inventory
                .Include(inv => inv.Product)
                .First(x => x.InventoryID == inventoryViewModel.InventoryID);

            if (inventory.Quantity >= inventoryViewModel.Quantity)
            {
                //If it greater or equal than: Add to order and decrease the inventory quantity

                Order order = GetOrderByUserIDOrderIDAndIsActive(UserID, StoreID);

                order.Date = DateTime.Now;

                //Verify if exists an orderDetail with the product to add the amount and no generating a second orderDetail


                    //( _storeDbContext.OrderDetails
                    //.Include(odetail => odetail.Product)
                    //.ToList()
                    //.Exists( x => x.OrderDetailID.== inventory.Product.ProductID ) )

                if (order.OrderDetails.Exists( x => x.Product.ProductID == inventory.Product.ProductID) )
                {
                    //Get the OrderDetail and add the new quantity to it
                    OrderDetail orderDetail = _storeDbContext.OrderDetails.First(x => x.Product.ProductID == inventory.Product.ProductID);
                    orderDetail.Quantity += inventoryViewModel.Quantity;

                    order.GetTotalAmountFromOrderDetail();

                    inventory.Quantity -= inventoryViewModel.Quantity;
                    _storeDbContext.Orders.Update(order);
                    _storeDbContext.Inventory.Update(inventory);

                    SaveChangesToDb();
                }
                else
                {
                    //Create a new OrderDetail
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        Product = inventory.Product,
                        Quantity = inventoryViewModel.Quantity,
                    };

                    order.OrderDetails.Add(orderDetail);

                    //Decrease the inventory from the store
                    inventory.Quantity -= inventoryViewModel.Quantity;

                    order.GetTotalAmountFromOrderDetail();


                    _storeDbContext.Orders.Update(order);
                    _storeDbContext.Inventory.Update(inventory);

                    SaveChangesToDb();
                }
                
            }
            else
            {
                throw new Exception("The input quantity is greater than the actual inventory quantity.");
            }

        }

        public List<Order> GetAllTheOrdersByLoggedCustomer(int UserID)
        {
            List<Order> orders = _storeDbContext.Orders
                .Include(order => order.Customer)
                .Include(order => order.Location)
                .Where(x => x.Customer.Id == UserID)
                .ToList();
            return orders;
        }

        public OrderViewModel ConvertOrderIntoVM(Order order)
        {
            OrderViewModel orderViewModel = _mapper.ConvertOrderIntoOrderVM(order);
            return orderViewModel;

        }

        public void CompleteOrder(int OrderID)
        {
            Order order = GetOrderByID(OrderID);

            if (order.TotalAmount == 0)
            {
                //means no product...
                //idk
            }
            else
            {
                //Finish the order
                order.IsCartActive = false;

                _storeDbContext.Orders.Update(order);

                SaveChangesToDb();
            }

        }

        public Order GetOrderByID(int OrderID)
        {
            Order order = _storeDbContext.Orders
                .First(x => x.OrderID == OrderID);
            return order;
        }

        public List<OrderDetail> GetAllTheOrderDetailByOrderID(int OrderID)
        {
            Order order = _storeDbContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(odetail => odetail.Product)
                .First(x => x.OrderID == OrderID);

            return order.OrderDetails;
        }
        public OrderDetailViewModel ConvertOrderDetailIntoVM(OrderDetail orderDetail)
        {
            OrderDetailViewModel orderDetailViewModel = _mapper.ConvertOrderDetailIntoOrderDetailVM(orderDetail);
            return orderDetailViewModel;
        }

        public List<Order> GetAllTheOrdersByCurrentLocation(int StoreID)
        {
            List<Order> orders = _storeDbContext.Orders
                .Include(order => order.Customer)
                .Include(order => order.Location)
                .Include(order => order.OrderDetails)
                .ThenInclude(odetail => odetail.Product)
                .Where(x => x.Location.LocationID == StoreID)
                .ToList();
            return orders;
        }

    }
}
