using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using VideogameShopLibrary.CVS_Models;

namespace VideogameShopLibrary.Services
{
    public class DisplayDbData
    {
        public StringBuilder DisplayAllData { get; private set; }
        public List<Product_Char> Product_Chars { get; private set; }
        public DisplayDbData(StringBuilder displayAllData)
        {
            DisplayAllData = displayAllData;
        }

        public DisplayDbData(List<Product_Char> product_Chars)
        {
            Product_Chars = product_Chars;
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

        public List<Product_Char> displayProductCharacteristics()
        {
            
            using(SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
            {
                sqlConnection.Open();
                using(SqlCommand cmd = new SqlCommand("SELECT * FROM P_Categories"))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Product_Char p_category = new Product_Char();
                            p_category.Category = reader.GetValue(reader.GetOrdinal("Categories")).ToString();
                            Product_Chars.Add(p_category);
                        }

                    }
                }
            }
        }
        
    }
}
