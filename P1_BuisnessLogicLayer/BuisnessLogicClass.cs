using P1_ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P1_RepositoryLayer;
using P1_ModelLib.ViewModels;

namespace P1_BuisnessLogicLayer
{
    public class BuisnessLogicClass
    {

        private readonly Repository _repository;


        public BuisnessLogicClass(Repository repository)
        {
            _repository = repository;

        }
        public Location GetDefaultLocationForRegisterCustomer()
        {
            Location myLocation = _repository.GetDefaultLocationForRegisterCustomer();

            return myLocation;
        }

        public List<ProductViewModel> GetAllTheProducts()
        {
            List<ProductViewModel> Products = _repository.GetAllTheProducts();
            return Products;
        }

        public Product CreateNewProduct(Product myProduct)
        {
            Product DbProduct = _repository.CreateNewProduct(myProduct);

            return DbProduct;
        }

        public void UpdateProduct(Product myProduct)
        {
            _repository.UpdateProduct(myProduct);
        }

        public void DeleteProduct(int id)
        {
            _repository.DeleteProduct(id);
        }
        public Product GetProductByID(int id)
        {
            Product myProduct =_repository.GetProductByID(id);
            return myProduct;
        }

        public ProductViewModel ConvertProductIntoVM(Product myProduct)
        {
            ProductViewModel myProductVM = _repository.ConvertProductIntoVM(myProduct);
            return myProductVM;
        }


        ///---------------------------------------------------------

        public List<InventoryViewModel> GetAllTheInventoryFromStore(int storeID)
        {
            List<Inventory> StoreInventory = _repository.GetAllTheInventoryFromStore(storeID);

            List<InventoryViewModel> storeInventoryVM = new List<InventoryViewModel>();

            foreach (Inventory inv in StoreInventory)
            {
                storeInventoryVM.Add( _repository.ConvertInventoryIntoVM(inv) );
            }

            return storeInventoryVM;
        }

        public Inventory GetInventoryFromStoreByID(int id, int StoreID)
        {
            List<Inventory> StoreInventory = _repository.GetAllTheInventoryFromStore(StoreID);

            Inventory inventory = StoreInventory.First(x => x.InventoryID == id);
            return inventory;
        }

        public InventoryViewModel ConvertInventoryIntoVM( Inventory inventory )
        {
            InventoryViewModel inventoryViewModel = _repository.ConvertInventoryIntoVM(inventory);
            return inventoryViewModel;
        }

        public Inventory CreateNewInventory(InventoryViewModel inventoryViewModel, int ProductID, int StoreID)
        {
            try
            { 
                Inventory inventory = _repository.CreateNewInventory(inventoryViewModel, ProductID, StoreID);
                return inventory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateInventoryQuantity(int id, int StoreID, int Quantity)
        {
            _repository.UpdateInventoryQuantity(id, StoreID, Quantity);
        }

        public void DeleteInventory(int id, int StoreID)
        {
            _repository.DeleteInventory(id, StoreID);
        }

        ///---------------------------------------------------------
        ///

        public List<Department> GetAllTheDepartments()
        {
            List<Department> departments = _repository.GetAllTheDepartments();
            return departments;
        }
        public Department GetDepartmentByID(int id)
        {
            Department department = _repository.GetDepartmentByID(id);
            return department;
        }

        public void CreateNewDepartment(Department department)
        {
            _repository.CreateNewDepartment(department);
        }

        public void UpdateDepartment(Department department)
        {
            _repository.UpdateDepartment(department);
        }

        public void DeleteDepartment(int id)
        {
            _repository.DeleteDepartment(id);
        }

        ///-----------------------------------------------------------
        ///

        public List<LocationViewModel> GetAllTheLocation()
        {
            List<LocationViewModel> locations = _repository.GetAllTheLocation();
            return locations;
        }

        public Location GetLocationByID(int id)
        {
            Location location = _repository.GetLocationByID(id);
            return location;
        }

        public LocationViewModel ConvertLocationIntoVM(Location location)
        {
            LocationViewModel locationViewModel = _repository.ConvertLocationIntoVM(location);
            return locationViewModel;
        }

        public void CreateNewLocation(LocationViewModel locationViewModel)
        {
            _repository.CreateNewLocation(locationViewModel);
        }

        public void UpdateLocation( int LocationId, LocationViewModel locationViewModel )
        {
            Location myLocation = this.GetLocationByID(LocationId);

            myLocation.Name = locationViewModel.Name;
            myLocation.Address = locationViewModel.Address;

            this.UpdateLocation(myLocation);
        }
        public void UpdateLocation(Location location)
        {
            _repository.UpdateLocation(location);
        }

        public void DeleteLocation(int id)
        {
            Location location = this.GetLocationByID(id);

            _repository.DeleteLocation(location);
        }

        ///-----------------------------------------------------------
        ///

        public List<Customer> GetAllTheCustomers()
        {
            List<Customer> customers = _repository.GetAllTheCustomers();
            return customers;
        }

        public CustomerViewModel ConvertCustomerIntoVM(Customer customer)
        {
            CustomerViewModel customerViewModel = _repository.ConvertCustomerIntoVM(customer);
            return customerViewModel;
        }
        public Customer GetCustomerByID(int id)
        {
            Customer customer = _repository.GetCustomerByID(id);
            return customer;
        }
        public CustomerViewModel GetCustomerVMByID(int id)
        {
            Customer customer = GetCustomerByID(id);

            CustomerViewModel customerViewModel = _repository.ConvertCustomerIntoVM(customer);
            return customerViewModel;
        }
        public Customer GetLoggedUserByUserName(string UserEmail)
        {
            Customer customer = _repository.GetLoggedUserByUserName(UserEmail);
            return customer;
        }


        public void UpdateCustomer(int id, CustomerViewModel customerViewModel,int LocationID)
        {
            _repository.UpdateCustomer(id, customerViewModel, LocationID);
        }

        public void DeleteUser(int id,  string UserEmail)
        {
            try
            {
                _repository.DeleteUser(id, UserEmail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CreateOrUpdatePendingOrder(int UserID, int StoreID)
        {
            int OrderID = _repository.CreateOrUpdatePendingOrder(UserID, StoreID);
            return OrderID;
        }

        public void SetQuantityForOrder(InventoryViewModel inventoryViewModel,int StoreID,int UserID)
        {
            _repository.SetQuantityForOrder(inventoryViewModel, StoreID, UserID);
        }

        public List<OrderViewModel> GetAllTheOrdersByLoggedCustomer(int UserID)
        {
            List<Order> orders = _repository.GetAllTheOrdersByLoggedCustomer(UserID);

            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();

            foreach (Order o in orders)
            {
                orderViewModels.Add( _repository.ConvertOrderIntoVM(o) );
            }
            return orderViewModels;
        }

        public void CompleteOrder(int OrderID)
        {
            _repository.CompleteOrder(OrderID);
        }

        public List<OrderDetailViewModel> GetAllTheOrderDetailByOrderID(int OrderID)
        {
            List<OrderDetail> orderDetails = _repository.GetAllTheOrderDetailByOrderID(OrderID);

            List<OrderDetailViewModel> orderDetailViewModels = new List<OrderDetailViewModel>();

            foreach (OrderDetail oDetail in orderDetails)
            {
                orderDetailViewModels.Add(_repository.ConvertOrderDetailIntoVM(oDetail));
            }

            return orderDetailViewModels;

        }

        public List<OrderViewModel> GetAllTheOrdersByCurrentLocation(int StoreID)
        {
            List<Order> orders = _repository.GetAllTheOrdersByCurrentLocation(StoreID);

            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();

            foreach (Order o in orders)
            {
                orderViewModels.Add(_repository.ConvertOrderIntoVM(o));
            }
            return orderViewModels;
        }

    }
}

