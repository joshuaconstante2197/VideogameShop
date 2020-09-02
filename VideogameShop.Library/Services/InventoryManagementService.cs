using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ChoETL;
using VideogameShopLibrary.CVS_Models;

namespace VideogameShopLibrary
{
    public class InventoryManagementService
    {
        public StringBuilder JsonSb { get; private set; }

        public InventoryManagementService()
        {

        }
        public InventoryManagementService(StringBuilder jsonSb)
        {
            JsonSb = jsonSb;
        }

        /// <summary>
        /// Processes the inventory file from a CSV to JSON file
        /// </summary>
        public void ProcessProductsFromCsv(string inputFileName)
        {
            using(var records = new ChoCSVReader<Product>(inputFileName)
                .WithFirstLineHeader()
                )
            {
                using (var f = new ChoJSONWriter(JsonSb))
                    f.Write(records);
            }
        }

        /// <summary>
        /// Processes the sales file from a CSV to JSON file
        /// </summary>
        public void ProcesOrdersFromCsv(string inputFileName)
        {
            using (var records = new ChoCSVReader<Order>(inputFileName)
                .WithFirstLineHeader()
                )
            {
                using (var f = new ChoJSONWriter(JsonSb))
                    f.Write(records);
            }
        }

        /// <summary>
        /// Saves Inventory CSV data into database
        /// </summary>
        public void SaveCsvInventory(string inputFileName)
        {
            SqlCommand cmd;
            var id = 0;

            foreach(var product in new ChoCSVReader<Product>(inputFileName)
                .WithFirstLineHeader()
                )
            {
                id++;
                var sql = "INSERT INTO Inventory(productId, [Game Title], Category, Platform, [Available Units], Cost, Price, Condition, [Product Type])" +
                    $"VALUES({id}, '{product.GameTitle}', '{product.Category}',  '{product.Platform}', {product.AvailableUnits}," +
                    $"{product.Cost} , {product.Price},  '{product.Condition}',  '{product.ProductType}' )";
                using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand(sql, sqlConnection);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Saves Sales CSV data into database, autocreates new Id per each sale, removes 1 item from inventory where Game Title matches
        /// </summary>
        public void SaveCsvOrders(string inputFileName)
        {
            
            SqlCommand cmd;
            int id;
            
            //insert each item to database
            foreach (var order in new ChoCSVReader<Order>(inputFileName)
                .WithFirstLineHeader()
                )
            {
                //Autoincrements new Id per each sale
                var getMax = @"SELECT COALESCE(MAX(orderId), 0) + 1 AS maxPlusOne FROM Sales";

                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    
                    sqlCon.Open();
                    cmd = new SqlCommand(getMax, sqlCon);
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }

                //inserts each item into database
                var sql = "INSERT INTO Sales(orderId, Product, Condition, Date, Total, [Customer Name], [Customer Phone], Email)" +
                    $"VALUES({id}, '{order.Product}', '{order.Condition}', '{order.Date}', {order.Total}, '{order.CustomerName}', '{order.CustomerPhone}'," +
                    $"'{order.Email}')";

                using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand(sql, sqlConnection);
                    cmd.ExecuteNonQuery();
                }

                //Removes 1 from inventory where Game Title matches 
                var sql2 = $"UPDATE Inventory SET [Available Units] = [Available Units] - 1 WHERE [Game Title] = '{order.Product}'";

                using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand(sql2, sqlConnection);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DropAllData()
        {
            SqlCommand cmd;
            var sql = "DELETE FROM Inventory; DELETE FROM Sales;";

            using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
            {
                sqlConnection.Open();
                cmd = new SqlCommand(sql, sqlConnection);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
