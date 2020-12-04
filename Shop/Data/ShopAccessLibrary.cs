using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows;
using Shop.Models;

namespace Shop
{
    class ShopAccessLibrary
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        internal List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ProductID, ProductName, Gender, Brands.BrandName, Type, Colors.ColorName, Sizes.SizeName, Price, Amount FROM Products join Colors_Sizes on Colors_Sizes.Color_SizeID = Products.Color_SizeID join Colors on Colors_Sizes.ColorID = Colors.ColorID join Sizes on Colors_Sizes.SizeID = Sizes.SizeID join Brands on Products.BrandID = Brands.BrandID", connection);
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
                        reader.GetValue(6).ToString(),
                        (float)Convert.ToDouble(reader.GetValue(7)),
                        Convert.ToInt32(reader.GetValue(8))));
                }
            }
            return products;
        }
        internal List<Product> SearchProductByCharacteristics(string name, string gender, string brand, string type, string color, string size, string price, string amount)
        {
            List<Product> products = new List<Product>();

            string nameForQuery = string.Empty;
            string brandForQuery = string.Empty;
            string genderForQuery = string.Empty;
            string typeForQuery = string.Empty;
            string colorForQuery = string.Empty;
            string sizeForQuery = string.Empty;
            string priceForQuery = string.Empty;
            string amountForQuery = string.Empty;

            List<string> chars = new List<string>();

            if (name != null)
            {
                nameForQuery = $"Products.ProductName like '%{name}%'";
                chars.Add(nameForQuery);
            }
            if (brand != null)
            {
                brandForQuery = $"Brands.BrandName = '{brand}'";
                chars.Add(brandForQuery);
            }
            if (gender != null)
            {
                genderForQuery = $"Products.Gender = '{gender}'";
                chars.Add(genderForQuery);
            }
            if (type != null)
            {
                typeForQuery = $"Type = '{type}'";
                chars.Add(typeForQuery);
            }
            if (color != null)
            {
                colorForQuery = $"Colors.ColorName = '{color}'";
                chars.Add(colorForQuery);
            }
            if (size != null)
            {
                sizeForQuery = $"Sizes.SizeName = '{size}'";
                chars.Add(sizeForQuery);
            }
            if (price != null)
            {
                priceForQuery = $"Products.Price = {price}";
                chars.Add(priceForQuery);
            }
            if (amount != null)
            {
                amountForQuery = $"Products.Amount = {amount}";
                chars.Add(amountForQuery);
            }

            string searchQuery = $"SELECT ProductID, ProductName, Gender, Brands.BrandName, Type, Colors.ColorName, Sizes.SizeName, Price, Amount FROM Products join Colors_Sizes on Colors_Sizes.Color_SizeID = Products.Color_SizeID join Colors on Colors_Sizes.ColorID = Colors.ColorID join Sizes on Colors_Sizes.SizeID = Sizes.SizeID join Brands on Products.BrandID = Brands.BrandID where ";

            int counter = 0;

            foreach (string characteristic in chars)
            {
                if (counter > 0)
                {
                    searchQuery += $" AND {characteristic}";
                }
                else
                {
                    searchQuery += $"{characteristic}";
                    counter += 1;
                }
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
                        reader.GetValue(6).ToString(),
                        (float)Convert.ToDouble(reader.GetValue(7)),
                        Convert.ToInt32(reader.GetValue(8))));
                }
            }
            return products;
        }
        internal Product GetProductById(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"Select ProductID, ProductName, Gender, Brands.BrandName, Type, Colors.ColorName, Sizes.SizeName, Price, Amount from Products join Brands on Brands.BrandID = Products.BrandID join Colors_Sizes on Products.Color_SizeID = Colors_Sizes.Color_SizeID join Colors on Colors.ColorID = Colors_Sizes.ColorID join Sizes on Sizes.SizeID = Colors_Sizes.SizeID where ProductID = {productId}", connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return new Product(
                    Convert.ToInt32(reader.GetValue(0)),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(),
                    reader.GetValue(3).ToString(),
                    reader.GetValue(4).ToString(),
                    reader.GetValue(5).ToString(),
                    reader.GetValue(6).ToString(),
                    (float)Convert.ToDouble(reader.GetValue(7)),
                    Convert.ToInt32(reader.GetValue(8)));
            }
        }
        internal void AddProduct(string name, string gender, string brand, string type, string color, string size, string price, string amount, byte[] image, string imageName)
        {
            if(AddProductImage(image, imageName) == false)
            {
                return;
            }
            int imageId = GetProductImageId(imageName);
            int brandId = Convert.ToInt32(GetBrandId(brand));
            int colorId = Convert.ToInt32(GetColorId(color));
            int sizeId = Convert.ToInt32(GetSizeId(size));
            int color_sizeId = Convert.ToInt32(GetColor_SizeId(colorId, sizeId));

            string addProductQuery = $"insert into Products values ('{name}', {brandId}, '{type}', {color_sizeId}, {price}, {amount}, '{gender}', {imageId})";

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
                MessageBox.Show(ex.Message + " ОШИБКА ТУТ --> AddProduct", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal bool AddProductImage(byte[] image, string name)
        {
            string addProductImageQuery = $"insert into ProductImages (Image, ImageName) values (@Image, @ImageName)";
            if (IsProductImageNameUniq(name))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(addProductImageQuery, connection);
                        command.Parameters.AddWithValue("@Image", image);
                        command.Parameters.AddWithValue("@ImageName", name);
                        command.ExecuteNonQuery();
                        return true;
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " ОШИБКА ТУТ --> AddProductImage", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Изображение с данным именем уже есть в Базе данных. Хотите использовать уже имеющееся изображение для данного товара?", "Недопустимое имя файла", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        internal int GetProductImageId(string name)
        {
            string getImageIdQuery = $"select ImageID from ProductImages where ImageName = '{name}'";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(getImageIdQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader.GetValue(0).ToString());
                    }
                    else return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ОШИБКА ТУТ --> GetProductImageId", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }
        internal bool IsProductImageNameUniq(string name)
        {
            string uniqCheckQuery = $"select ImageName from ProductImages where ImageName = '{name}'";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(uniqCheckQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return false;
                    }
                    else return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ОШИБКА ТУТ --> GetProductImageId", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        internal string GetProductImageName(int productId)
        {
            string query = $"select ImageName from ProductImages join Products on Products.ImageID = ProductImages.ImageID where Products.ProductID = {productId}";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return reader.GetValue(0).ToString();
                    }
                    else return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ОШИБКА ТУТ --> GetProductImageId", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        internal byte[] GetProductPhoto(Product product)
        {
            if(product == null)
            {
                product = new Product(6, "df", "df", "df", "df", "df", "df", 123456, 2);
            }
            byte[] image;
            ShopAccessLibrary library = new ShopAccessLibrary();
            string photoName = library.GetProductImageName(product.ProductId);
            string selectPhotoQuery = $"Select Image from ProductImages where ImageName = '{photoName}'";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(selectPhotoQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    if (reader.Read())
                    {
                        ImageConverter converter = new ImageConverter();
                        image = (byte[])reader.GetValue(0);
                        return image;
                    }
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message + " " + "Ошибка тут -> GetProductPhoto()", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return null;
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
            string colorIdQuery = $"select Colors.ColorID from Colors where Colors.ColorName = '{color}'";
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
            string sizeIdQuery = $"select Sizes.SizeID from Sizes where Sizes.SizeName = '{size}'";
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

        internal List<ActiveOrder> GetActiveOrders()
        {
            List<ActiveOrder> orders = new List<ActiveOrder>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select distinct Orders.OrderID, Managers.ManagerLName, Managers.ManagerFName, Customers.CustomerLName, Customers.CustomerFName, Orders.OrderDate from Orders join Managers on Orders.ManagerID = Managers.ManagerID join Customers on Orders.CustomerID = Customers.CustomerID where Orders.Status = 'Не выполнен'", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string customerFullName = reader.GetValue(1).ToString() + " " + reader.GetValue(2).ToString();
                    string managerFullName = reader.GetValue(3).ToString() + " " + reader.GetValue(4).ToString();
                    orders.Add(new ActiveOrder(
                        Convert.ToInt32(reader.GetValue(0)),
                        customerFullName,
                        managerFullName,
                        reader.GetValue(5).ToString()
                        ));
                }
            }

            return orders;
        }
        internal List<ActiveOrder> GetOrderById(int id)
        {
            List<ActiveOrder> order = new List<ActiveOrder>();

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"Select Orders.OrderID, Managers.ManagerLName, Managers.ManagerFName, Customers.CustomerLName, Customers.CustomerFName, Orders.OrderDate from Orders join Managers on Orders.ManagerID = Managers.ManagerID join Customers on Orders.CustomerID = Customers.CustomerID where Orders.OrderID = {id}", connection);
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        string customerFullName = reader.GetValue(1).ToString() + " " + reader.GetValue(2).ToString();
                        string managerFullName = reader.GetValue(3).ToString() + " " + reader.GetValue(4).ToString();
                        order.Add(new ActiveOrder(
                            Convert.ToInt32(reader.GetValue(0)),
                            customerFullName,
                            managerFullName,
                            reader.GetValue(5).ToString()
                            ));
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                return order;
            }
        }
    }
}