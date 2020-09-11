using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using VideogameShopLibrary;
using VideogameShopLibrary.CVS_Models;

namespace VideogameShop.Library.Services
{
    public class RetrieveDbData
    {
        public class ProductCharacteristics
        {
            public List<string> Category { get;  set; }
            public List<string> Condition { get;  set; }
            public List<string> Platform { get;  set; }
            public List<string> ProductType { get;  set; }
        }

        public ProductCharacteristics displayProductCharacteristics()
        {
            ProductCharacteristics productCharacteristics = new ProductCharacteristics();

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
                            productCharacteristics.ProductType.Add(reader.GetValue(reader.GetOrdinal("Product Type")).ToString());
                        }
                    }
                }

            }
            return productCharacteristics;
        }
    }
}
