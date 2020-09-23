using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    class ActiveOrder
    {
        public int ActiveOrderId { get; }
        public string ActiveOrderCustomer { get; set; }
        public string ActiveOrderManager { get; set; }
        public string ActiveOrderDate { get; set; }

        public ActiveOrder(int id, string activeOrderCustomer, string activeOrderManager, string activeOrderDate)
        {
            ActiveOrderId = id;
            ActiveOrderCustomer = activeOrderCustomer;
            ActiveOrderManager = activeOrderManager;
            ActiveOrderDate = activeOrderDate;
        }
    }
}
