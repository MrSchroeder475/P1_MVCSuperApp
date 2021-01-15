using System.ComponentModel.DataAnnotations;

namespace P1_ModelLib.Models
{
    public class Product
    {
        private int intProductID;
        [Key]
        public int ProductID
        {
            get { return intProductID; }
            set { intProductID = value; }
        }
        private string strName;
        [StringLength(35, ErrorMessage = "The product name must be from 5 to 20 characters.", MinimumLength = 5)]
        //[RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "Product Name")]
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
        private double dblPrice;
        [Required]
        [Range(0.0,double.MaxValue,ErrorMessage ="The price must have a positive value.")]
        public double Price
        {
            get { return dblPrice; }
            set { dblPrice = value; }
        }
        private string strDescription;
        [StringLength( maximumLength: 180, ErrorMessage = "The description name must be from 5 to 90 characters.", MinimumLength = 5)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }

        private Department department;

        [Required]
        public Department Department
        {
            get { return department; }
            set { department = value; }
        }


    }
}