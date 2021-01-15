
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace P1_ModelLib.Models
{
    public class Customer : IdentityUser<int>
    {
        //private int intCustomerID;
        //[Key]
        //public int CustomerID
        //{
        //    get { return intCustomerID; }
        //    set { intCustomerID = value; }
        //}
        private string strFirstName;

        [StringLength(20, ErrorMessage = "The first name must be from 3 to 20 characters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName
        {
            get { return strFirstName; }
            set { strFirstName = value; }
        }
        private string strLastName;
        [StringLength(20, ErrorMessage = "The last name must be from 3 to 20 characters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName
        {
            get { return strLastName; }
            set { strLastName = value; }
        }

        //private string strEmail;
        //[EmailAddress(ErrorMessage ="Invalid Email")]
        //[Required]
        //[Display(Name ="User Email")]
        //public string Email
        //{
        //    get { return strEmail; }
        //    set { strEmail = value; }
        //}

        //private string strPassword;

        //[StringLength(25, ErrorMessage = "The password must be from 5 to 25 characters.", MinimumLength = 3)]
        //[DataTypeAttribute(DataType.Password)]
        //[Required]

        //public string Password
        //{
        //    get { return strPassword; }
        //    set { strPassword = value; }
        //}


        private Location objLocation;
        public Location Location
        {
            get { return objLocation; }
            set { objLocation = value; }
        }
        /// <summary>
        /// Overrided method ToString, it returns the complete name of the customer
        /// </summary>
        /// <returns>Returns the full name of the customer with space between the first and last names.</returns>
        public override string ToString()
        {
            return $"{strFirstName} {this.strLastName}";
        }


        public void SetLocationForCustomer( Location location )
        {
            //...
            Location = location;
        }

        
    }
}