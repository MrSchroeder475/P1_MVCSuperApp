using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace P1_ModelLib.ViewModels
{
    public class ProductViewModel
    {
        [StringLength(35, ErrorMessage = "The product name must be from 5 to 20 characters.", MinimumLength = 5)]
        //[RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }



        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The price must have a positive value.")]
        public double Price { get; set; }

        [StringLength(maximumLength: 180, ErrorMessage = "The description name must be from 5 to 90 characters.", MinimumLength = 5)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        public string Description { get; set; }

        public string Department { get; set; }

        public int ProductID { get; set; }

    }
}
