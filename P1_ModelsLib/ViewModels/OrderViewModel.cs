using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_ModelLib.ViewModels
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }


        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; }


        public int LocationID { get; set; }
        
        [Display(Name ="Store Name")]
        public string StoreName { get; set; }


        //We are going to pass the id for the List of orderDetails 

        public double  TotalAmount { get; set; }

        public DateTime Date { get; set; }

        [Display(Name ="Pending Order   ")]
        public bool isCartActive { get; set; }

    }
}
