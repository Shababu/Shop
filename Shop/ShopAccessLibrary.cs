using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows;
using Shop.Models;

namespace Shop
{
    class ShopAccessLibrary
    {
        private string _connectionString = @"Data Source=WIN-MTM9TBC96DI\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True";

        internal List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ProductID, ProductName, Brands.BrandName, Type, Colors.Color, Sizes.Size, Price, Amount FROM Products join Colors_Sizes on Colors_Sizes.Color_SizeID = Products.Color_SizeID join Colors on Colors_Sizes.ColorID = Colors.ColorID join Sizes on Colors_Sizes.SizeID = Sizes.SizeID join Brands on Products.BrandID = Brands.BrandID", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product(
                        Convert.ToInt32(reader.GetValue(0)),
                        reader.GetValue(1).ToString(),
                        reader.GetValue(2).ToString(),
                        reader.GetValue(3).ToString(),
                        reader.GetValue(4).ToString(),
                        reader.GetValue(5).ToString(),
                        (float)Convert.ToDouble(reader.GetValue(6)),
                        Convert.ToInt32(reader.GetValue(7))));
                }
            }
            return products;
        }

        internal List<Order> GetOrdersToComplete()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select distinct Orders.OrderID, Managers.ManagerLName, Managers.ManagerFName, Orders.OrderDate, Orders.Status, Orders.CompletionDate from Orders_Products join Orders on Orders_Products.OrderID = Orders.OrderID join Managers on Orders.ManagerID = Managers.ManagerID where Orders.Status = 'Не выполнен'", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DateTime completionDate = new DateTime();

                    if (reader.GetValue(5) == null)
                    {
                        completionDate = DateTime.MinValue;
                    }
                    orders.Add(new Order(Convert.ToInt32(reader.GetValue(0)),
                        new Manager(default, reader.GetValue(2).ToString(), reader.GetValue(1).ToString(), default, default, null),
                        new Customer(),
                        DateTime.Parse(reader.GetValue(3).ToString()),
                        reader.GetValue(4).ToString(),
                        completionDate
                        ));
                }
            }

            return orders;
        }

        internal Product GetProductById(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"Select ProductID, ProductName, Brands.BrandName, Type, Colors.Color, Sizes.Size, Price, Amount from Products join Brands on Brands.BrandID = Products.BrandID join Colors_Sizes on Products.Color_SizeID = Colors_Sizes.Color_SizeID join Colors on Colors.ColorID = Colors_Sizes.ColorID join Sizes on Sizes.SizeID = Colors_Sizes.SizeID where ProductID = {productId}");
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return new Product(
                    Convert.ToInt32(reader.GetValue(0)),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(),
                    reader.GetValue(3).ToString(),
                    reader.GetValue(4).ToString(),
                    reader.GetValue(5).ToString(),
                    (float)Convert.ToDouble(reader.GetValue(6)),
                    Convert.ToInt32(reader.GetValue(7)));
            }
        }

        internal void AddProduct(string name, string brand, string type, string color, string size, string price, string amount)
        {            
            int brandId = Convert.ToInt32(GetBrandId(brand));
            int colorId = Convert.ToInt32(GetColorId(color));
            int sizeId = Convert.ToInt32(GetSizeId(size));
            int color_sizeId = Convert.ToInt32(GetColor_SizeId(colorId, sizeId));

            string addProductQuery = $"insert into Products values ('{name}', {brandId}, '{type}', {color_sizeId}, {price}, {amount})";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(addProductQuery, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Товар был успешно добавлен в базу данных", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        internal int GetBrandId(string brand)
        {
            string brandIdQuery = $"select Brands.BrandID from Brands where Brands.BrandName = '{brand}'";
            int brandId = default;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(brandIdQuery, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    brandId = Convert.ToInt32(reader.GetValue(0));
                }
            }

            return brandId;
        }
        internal int GetColorId(string color)
        {
            string colorIdQuery = $"select Colors.ColorID from Colors where Colors.Color = '{color}'";
            int colorId = default;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(colorIdQuery, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    colorId = Convert.ToInt32(reader.GetValue(0));
                }
            }

            return colorId;
        }
        internal int GetSizeId(string size)
        {
            string sizeIdQuery = $"select Sizes.SizeID from Sizes where Sizes.Size = '{size}'";
            int sizeId = default;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sizeIdQuery, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sizeId = Convert.ToInt32(reader.GetValue(0));
                }
            }

            return sizeId;
        }
        internal int GetColor_SizeId(int colorId, int sizeId)
        {
            string color_sizeIdQuery = $"select Color_SizeID from Colors_Sizes join Colors on Colors.ColorID = Colors_Sizes.ColorID join Sizes on Sizes.SizeID = Colors_Sizes.SizeID where Colors.ColorID = '{colorId}' AND Sizes.SizeID = '{sizeId}'";
            int color_sizeId = default;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(color_sizeIdQuery, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    color_sizeId = Convert.ToInt32(reader.GetValue(0));
                }
            }

            return color_sizeId;
        }                                    
        internal List<Product> SearchProductByCharacteristics(string name, string brand, string type, string color, string size, string price, string amount)
        {
            List<Product> products = new List<Product>();

            string nameForQuery = string.Empty;
            string brandForQuery = string.Empty;
            string typeForQuery = string.Empty;
            string colorForQuery = string.Empty;
            string sizeForQuery = string.Empty;
            string priceForQuery = string.Empty;
            string amountForQuery = string.Empty;

            List<string> chars = new List<string>();

            if (name != string.Empty)
            {
                nameForQuery = $"Products.ProductName = '{name}' or Products.ProductName like '{name}%' or Products.ProductName like '%{name}%' or Products.ProductName like '%{name}'";
                chars.Add(nameForQuery);
            }
            if (brand != string.Empty)
            {
                brandForQuery = $"Brands.BrandName = '{brand}'";
                chars.Add(brandForQuery);
            }
            if (type != string.Empty)
            {
                typeForQuery = $"Type = '{type}'";
                chars.Add(typeForQuery);
            }
            if (color != string.Empty)
            {
                colorForQuery = $"Colors.Color = '{color}'";
                chars.Add(colorForQuery);
            }
            if (size != string.Empty)
            {
                sizeForQuery = $"Sizes.Size = '{size}'";
                chars.Add(sizeForQuery);
            }
            if (price != string.Empty)
            {
                priceForQuery = $"Products.Price = {price}";
                chars.Add(priceForQuery);
            }
            if (amount != string.Empty)
            {
                amountForQuery = $"Products.Amount = {amount}";
                chars.Add(amountForQuery);
            }

            string searchQuery = $"SELECT ProductID, ProductName, Brands.BrandName, Type, Colors.Color, Sizes.Size, Price, Amount FROM Products join Colors_Sizes on Colors_Sizes.Color_SizeID = Products.Color_SizeID join Colors on Colors_Sizes.ColorID = Colors.ColorID join Sizes on Colors_Sizes.SizeID = Sizes.SizeID join Brands on Products.BrandID = Brands.BrandID where ";

            int counter = 0;

            foreach(string characteristic in chars)
            {
                if(counter > 0)
                {
                    searchQuery += $" AND {characteristic}";
                }
                else
                {
                    searchQuery += $"{characteristic}";
                    counter++;
                }
            }

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(searchQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product(
                        Convert.ToInt32(reader.GetValue(0)),
                        reader.GetValue(1).ToString(),
                        reader.GetValue(2).ToString(),
                        reader.GetValue(3).ToString(),
                        reader.GetValue(4).ToString(),
                        reader.GetValue(5).ToString(),
                        (float)Convert.ToDouble(reader.GetValue(6)),
                        Convert.ToInt32(reader.GetValue(7))));
                }
            }
            return products;
        }
    }
}
// Test