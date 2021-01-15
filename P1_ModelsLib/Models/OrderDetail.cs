using System.ComponentModel.DataAnnotations;

namespace P1_ModelLib.Models
{
    public class OrderDetail
    {
        private int intOrderDetailID;
        public int OrderDetailID
        {
            get { return intOrderDetailID; }
            set { intOrderDetailID = value; }
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
        [Range(1,99, ErrorMessage ="The minimum quantity for an order is 1 and the maximum is 99.")]
        public int Quantity
        {
            get { return intQuantity; }
            set { intQuantity = value; }
        }
        
        
    }
}