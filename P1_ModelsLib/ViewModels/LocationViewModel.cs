using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_ModelLib.ViewModels
{
    public class LocationViewModel
    {
        public int LocationID { get; set; }


        [StringLength(30, ErrorMessage = "The name must be from 5 to 30 characters.", MinimumLength = 5)]
        [Required]
        public string Name { get; set; }


        //View the Warehouse

        [Required]
        [StringLength(50, ErrorMessage = "The name must be from 5 to 50 characters.", MinimumLength = 5)]
        public string Address { get; set; }
    }
}
