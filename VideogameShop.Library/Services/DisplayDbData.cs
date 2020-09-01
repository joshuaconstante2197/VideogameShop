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
        
    }
}
