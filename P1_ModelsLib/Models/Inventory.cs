using System.ComponentModel.DataAnnotations;

namespace P1_ModelLib.Models
{
    public class Inventory
    {
        private int intInventoryID;
        public int InventoryID
        {
            get { return intInventoryID; }
            set { intInventoryID = value; }
        }
        
        private Product objProduct;
        [Required]
        public Product Product
        {
            get { return objProduct; }
            set { objProduct = value; }
        }

        private int intQuantity;
        [Required]
        [Range(0,99,ErrorMessage ="The minimum quantity is up to 0 and the maximum quantity of the inventory is up to 99.")]
        public int Quantity
        {
            get { return intQuantity; }
            set { intQuantity = value; }
        }


        // Verify Deals...


    }
}