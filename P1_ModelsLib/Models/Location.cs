using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace P1_ModelLib.Models
{
    public class Location : ISortBy
    {
        private int intLocationID;
        public int LocationID
        {
            get { return intLocationID; }
            set { intLocationID = value; }
        }
        private string strName;
        [StringLength(30, ErrorMessage = "The name must be from 5 to 30 characters.", MinimumLength = 5)]
        [Required]
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
        private List<Inventory> objInventory;
        public List<Inventory> Inventory
        {
            get { return objInventory; }
            set { objInventory = value; }
        }


        private string strAddress1;
        [Required]
        [StringLength(50, ErrorMessage = "The name must be from 5 to 50 characters.", MinimumLength = 5)]
        public string Address
        {
            get { return strAddress1; }
            set { strAddress1 = value; }
        }

        IEnumerable<object> ISortBy.SortBy(EnumSortOptions sortOptions)
        {
            //////
            switch (sortOptions)
            {
                case EnumSortOptions.Ascending:
                    IEnumerable<object> AscendListInventory = this.Inventory.OrderBy(x => x.InventoryID);
                    return AscendListInventory;
                case EnumSortOptions.Descending:
                    IEnumerable<object> DescendListInventory = this.Inventory.OrderByDescending(x => x.InventoryID);
                    return DescendListInventory;
                default:
                    break;
            }
            return null;
        }

    }
}