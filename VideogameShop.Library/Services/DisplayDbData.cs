using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using VideogameShop.Library.Services;
using VideogameShopLibrary.CVS_Models;

namespace VideogameShopLibrary.Services
{
    public class DisplayDbData
    {
        public StringBuilder DisplayAllData { get; private set; }
        public List<Product> Products { get; private set; }
        public Product Product { get; private set; }

        //constructor to retrieve all data to the console
        public DisplayDbData(StringBuilder displayAllData)
        {
            DisplayAllData = displayAllData;
        }
       
        

        
        public static StringBuilder displayAllData(StringBuilder DisplayAllData)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                DisplayAllData.Append(reader.GetName(i) + " : " + reader.GetValue(i));
                                DisplayAllData.Append("\n");
                            }
                            DisplayAllData.Append("\n");
                        }
                    }
                }
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Sales", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                DisplayAllData.Append(reader.GetName(i) + " : " + reader.GetValue(i));
                                DisplayAllData.Append("\n");
                            }
                            DisplayAllData.Append("\n");
                        }
                    }
                }
            }
            return DisplayAllData;
        }

        //method to return list of products
        public static List<Product> DisplayInventory(List<Product> Products)
        {
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory",sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Product product = new Product();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                //remove white spaces to match the Product model field names
                                var str = reader.GetName(i).Replace(" ","");
                                PropertyInfo propertyInfo = product.GetType().GetProperty(str);
                                //check if row is empty
                                if(propertyInfo != null && !reader.IsDBNull(i))
                                {
                                    propertyInfo.SetValue(product, reader.GetValue(i), null);
                                }
                            }
                            //add object to list of Product after it being filled with data from DB
                            Products.Add(product);
                        }
                    }
                }
            }
            return (Products);
        }
        //method to return a product from it's id
        public static Product GetProductById(Product product, int productId)
        {
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                var sql = $"SELECT * FROM Inventory Where (productId = {productId})";
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                //remove white spaces to match the Product model field names
                                var str = reader.GetName(i).Replace(" ", "");
                                PropertyInfo propertyInfo = product.GetType().GetProperty(str);
                                //check if row is empty
                                if (propertyInfo != null && !reader.IsDBNull(i))
                                {
                                    propertyInfo.SetValue(product, reader.GetValue(i), null);
                                }
                            }
                            //add object to list of Product after it being filled with data from DB
                        }
                    }
                }
            }
            return (product);
        }

        //Method to retrieve an object of lists with the product characteristics
        public static void DisplayProductCharacteristics(ProductCharacteristics productCharacteristics)
        {

            using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM P_Categories", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productCharacteristics.Category.Add(reader.GetValue(reader.GetOrdinal("Category")).ToString());
                        }
                    }
                }
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM P_Conditions", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productCharacteristics.Condition.Add(reader.GetValue(reader.GetOrdinal("Condition")).ToString());
                        }
                    }
                }
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM P_Platforms", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productCharacteristics.Platform.Add(reader.GetValue(reader.GetOrdinal("Platform")).ToString());

                        }
                    }
                }
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM P_Types", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productCharacteristics.ProductType.Add(reader.GetValue(reader.GetOrdinal("ProductType")).ToString());
                        }
                    }
                }

            }
        }
        //method to return list of orders to view
        public static List<Order> DisplayOrders(List<Order> orders, string sql)
        {
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                SqlCommand cmd;
                using (cmd = new SqlCommand(sql, sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var str = reader.GetName(i);
                                str = str.Replace(" ", "");
                                PropertyInfo propertyInfo = order.GetType().GetProperty(str);

                                if (propertyInfo != null && !reader.IsDBNull(i))
                                {
                                    propertyInfo.SetValue(order, reader.GetValue(i), null);
                                }
                            }
                            orders.Add(order);
                        }
                    }
                }
            }
            return (orders);
        }

    }
}
