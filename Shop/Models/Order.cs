using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    class Order
    {
        public int OrderId { get; set; }
        public Manager Manager { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public DateTime CompletionDate { get; set; }

        public Order(int id, Manager manager, Customer customer,
                       DateTime orderDate, string status, DateTime completionDate)
        {
            OrderId = id;
            Manager = manager;
            Customer = customer;
            OrderDate = orderDate;
            Status = status;
            CompletionDate = completionDate;
        }
    }
}
