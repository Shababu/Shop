using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerFName { get; set; }
        public string CustomerLName { get; set; }
        public int PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }

        public Customer(int id, string firstName, string lastName, int phoneNumber, string mail, string address)
        {
            CustomerId = id;
            CustomerFName = firstName;
            CustomerLName = lastName;
            PhoneNumber = phoneNumber;
            Mail = mail;
            Address = address;
        }
        public Customer()
        {
            CustomerId = 0;
            CustomerFName = "Null";
            CustomerLName = "Null";
            PhoneNumber = 0;
            Mail = "Null";
            Address = "Null";
        }
    }
}
