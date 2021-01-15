using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_ModelLib.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [StringLength(20, ErrorMessage = "The first name must be from 3 to 20 characters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        
        [StringLength(20, ErrorMessage = "The last name must be from 3 to 20 characters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Display(Name = "User Email")]
        public string Email { get; set; }

        [Display(Name ="Store Name")]
        public string LocationName { get; set; }
    }
}
