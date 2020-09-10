using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ChoETL;
using VideogameShop.Library.Models;
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
        /// Save Product Characteristics first so Foreing Key relationship is possible
        /// </summary>
        
        public void SaveProductChar(string inputFileName)
        {
            SqlCommand cmd;
            foreach (var productChar in new ChoCSVReader<Product_Char>(inputFileName)
                .WithFirstLineHeader()
                )
            {
                Console.WriteLine(productChar.Category);
                var sql = $"INSERT INTO P_Categories(Category) SELECT('{productChar.Category}') WHERE NOT EXISTS(SELECT * FROM P_Categories WHERE Category = '{productChar.Category}') " +
                          $"INSERT INTO P_Platforms(Platform) SELECT('{productChar.Platform}') WHERE NOT EXISTS(SELECT * FROM P_Platforms WHERE Platform = '{productChar.Platform}') " +
                          $"INSERT INTO P_Conditions(Condition) SELECT('{productChar.Condition}') WHERE NOT EXISTS(SELECT * FROM P_Conditions WHERE Condition = '{productChar.Condition}') " +
                          $"INSERT INTO P_Types([Product Type]) SELECT('{productChar.ProductType}') WHERE NOT EXISTS(SELECT * FROM P_Types WHERE [Product Type] = '{productChar.ProductType}')";
                using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand(sql, sqlConnection);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Saves Inventory CSV data into database
        /// </summary>

        public void SaveCsvInventory(string inputFileName)
        {
            SqlCommand cmd;

            foreach (var product in new ChoCSVReader<Product>(inputFileName)
                .WithFirstLineHeader()
                )
            {

                var sql = "INSERT INTO Inventory( [Game Title], Category, Platform, [Available Units], Cost, Price, Condition, [Product Type])" +
                    $"VALUES('{product.GameTitle}', '{product.Category}',  '{product.Platform}', {product.AvailableUnits}," +
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


            //insert each item to database
            foreach (var order in new ChoCSVReader<Order>(inputFileName)
                .WithFirstLineHeader()
                )
            {
                //Autoincrements new Id per each sale

                //inserts each item into database
                var sql = "INSERT INTO Sales(Product, Quantity, Condition, Date, Total, [Customer Name], [Customer Phone], Email)" +
                    $"VALUES('{order.Product}', {order.Quantity}, '{order.Condition}', '{order.Date}', {order.Total}, '{order.CustomerName}', '{order.CustomerPhone}'," +
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
            var sql = "DELETE FROM Inventory; DELETE FROM Sales; DELETE FROM P_Categories; DELETE FROM P_Conditions; DELETE FROM P_Platforms; DELETE FROM P_Types;";

            using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
            {
                sqlConnection.Open();
                cmd = new SqlCommand(sql, sqlConnection);
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("dropped al data");
        }

    }
}
