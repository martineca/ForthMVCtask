using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FourthTask.Models
{
    public class CustomerOrdersModel
    {
        public Customer CurrCustomer { get; set; }
        public IEnumerable<Order> CustomerOrders { get; set; }

    }
}