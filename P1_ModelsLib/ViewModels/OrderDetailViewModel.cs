using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_ModelLib.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderDetailID { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name ="Product Price")]
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }

        [Display(Name ="Product Total Amount")]
        public double TotalAmountDetail { get; set; }


    }
}
