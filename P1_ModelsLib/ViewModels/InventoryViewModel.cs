using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_ModelLib.ViewModels
{
    public class InventoryViewModel
    {
        public int InventoryID { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Display(Name ="Product Price")]
        public double ProductPrice { get; set; }

        [Required]
        [Range(0, 99, ErrorMessage = "The minimum quantity is up to 0 and the maximum quantity of the inventory is up to 99.")]
        public int Quantity { get; set; }   


    }
}
