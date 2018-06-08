using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FourthTask.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string Message { get; set; }
        public Nullable<int> quantity { get; set; }
    }
}