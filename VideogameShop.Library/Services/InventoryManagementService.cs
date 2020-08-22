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
                    $"{product.Cost},  {product.Price},  '{product.Condition}',  '{product.ProductType}' )";
                using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand(sql, sqlConnection);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void SaveCsvOrders(string inputFileName)
        {
            SqlCommand cmd;
            var id = 0;
            foreach (var order in new ChoCSVReader<Order>(inputFileName)
                .WithFirstLineHeader()
                )
            {
                id++;
                var sql = "INSERT INTO Sales(orderId, Product, Condition, Date, Total, [Customer Name], [Customer Phone], Email)" +
                    $"VALUES({id}, '{order.Product}', '{order.Condition}', '{order.Date}', {order.Total}, '{order.CustomerName}', '{order.CustomerPhone}'," +
                    $"'{order.Email}')";
                using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand(sql, sqlConnection);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
    }
}
