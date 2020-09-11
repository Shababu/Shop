using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    class Manager
    { 
        public int ManagerId { get; set; }
        public string ManagerFName { get; set; }
        public string ManagerLName { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public string Login { get; set; }

        public Manager(int id, string firstName, string lastName, int age, double salary, string login)
        {
            ManagerId = id;
            ManagerFName = firstName;
            ManagerLName = lastName;
            Age = age;
            Salary = salary;
            Login = login;
        }
    }
}
