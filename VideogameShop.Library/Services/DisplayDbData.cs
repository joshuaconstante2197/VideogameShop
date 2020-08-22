using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideogameShopLibrary.Services
{
    public class DisplayDbData
    {
        public static void displayAllData()
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
                                Console.WriteLine(reader.GetName(i) + " : " + reader.GetValue(i));
                                
                            }
                            Console.WriteLine();
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
                                Console.WriteLine(reader.GetName(i) + " : " + reader.GetValue(i));

                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
        
    }
}
