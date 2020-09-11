using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public string ProductType { get; set; }
        public string ProductColor { get; set; }
        public string ProductSize { get; set; }
        public float ProductPrice { get; set; }
        public int ProductAmount { get; set; }

        public Product(int id, string name, string brand, string type, 
                       string color, string size, float price, int amount)
        {
            ProductId = id;
            ProductName = name;
            ProductBrand = brand;
            ProductType = type;
            ProductColor = color;
            ProductSize = size;
            ProductPrice = price;
            ProductAmount = amount;
        }
    }
}
