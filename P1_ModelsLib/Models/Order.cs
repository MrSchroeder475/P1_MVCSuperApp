using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace P1_ModelLib.Models
{
    public class Order : ISortBy
    {
        private int intOrderID;
        public int OrderID
        {
            get { return intOrderID; }
            set { intOrderID = value; }
        }
        private Location objLocation;
        [Required]
        public Location Location
        {
            get { return objLocation; }
            set { objLocation = value; }
        }
        private List<OrderDetail> lstOrderDetails = new List<OrderDetail>();
        public List<OrderDetail> OrderDetails
        {
            get { return lstOrderDetails; }
            set { lstOrderDetails = value; }
        }
        private Customer objCustomer;
        [Required]
        public Customer Customer
        {
            get { return objCustomer; }
            set { objCustomer = value; }
        }
                
        private DateTime dtmDate;
        public DateTime Date
        {
            get { return dtmDate; }
            set { dtmDate = value; }
        }

        private double _dblTotalAmount;

        public double TotalAmount
        {
            get { return _dblTotalAmount; }
            set { _dblTotalAmount = value; }
        }

        private bool bolIsCartActive;
        public bool IsCartActive
        {
            get { return bolIsCartActive; }
            set { bolIsCartActive = value; }
        }



        /// <summary>
        /// This method is used to get all the TotalAmount from the OrderDetails.
        /// </summary>
        /// <returns>Returns a double parameter with the totalAmount.</returns>
        public void GetTotalAmountFromOrderDetail()
        {
            double result = 0;

            foreach (OrderDetail orderDetail in this.OrderDetails)
            {
                result += (orderDetail.Quantity * orderDetail.Product.Price);
            }

            _dblTotalAmount = result;
        }

        IEnumerable<object> ISortBy.SortBy(EnumSortOptions sortOptions)
        {
            switch (sortOptions)
            {
                case EnumSortOptions.Ascending:
                    IEnumerable<object> AscendListOrderD = this.OrderDetails.OrderBy(x => x.OrderDetailID);
                    return AscendListOrderD;

                case EnumSortOptions.Descending:
                    IEnumerable<object> DescendingListOrderD = this.OrderDetails.OrderByDescending(x => x.OrderDetailID);
                    return DescendingListOrderD;
                default:
                    return null;
            }

        }
    }
}