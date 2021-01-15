using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_ModelLib.Models
{
    public class Department
    {
        private int intDepartmentID;

        public int DepartmentID
        {
            get { return intDepartmentID; }
            set { intDepartmentID = value; }
        }

        private string strName;
        [StringLength(30, ErrorMessage = "The department name must be from 5 to 30 characters.", MinimumLength = 5)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

    }
}
