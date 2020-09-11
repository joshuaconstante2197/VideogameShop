using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using VideogameShop.Library.Services;


namespace VideogameShopLibrary.Services
{
    public class DisplayDbData
    {
        public StringBuilder DisplayAllData { get; private set; }

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
                            productCharacteristics.ProductType.Add(reader.GetValue(reader.GetOrdinal("Product Type")).ToString());
                        }
                    }
                }

            }
        }

    }
}
