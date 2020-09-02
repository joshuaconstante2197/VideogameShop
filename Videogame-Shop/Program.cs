
using System;
using System.Text;


using VideogameShopLibrary.Services;

namespace VideogameShopLibrary
{

    class Program
    {

        static void Main(string[] args)
        {
            string input;
            while (true)
            {
                Console.WriteLine("Type Import to upload data or Retrieve to display data from database");
                input = Console.ReadLine().Trim();

                if (input.ToLower() == "import" || input.ToLower() == "retrieve")
                    break;
            }
            

            if (input.ToLower() == "import")
            {
                while (true)
                {
                    Console.WriteLine("Type JSON to save data to a JSON file or SQL to save data to the database");
                    input = Console.ReadLine().Trim();
                    if (input.ToLower() == "json" || input.ToLower() == "sql")
                        break;
                }
               

                if (input.ToLower() == "json")
                {
                    Console.WriteLine("json input");
                    try
                    {
                        //string builder necessary to parse csv to JSON format

                        StringBuilder JInventoryrecords = new StringBuilder();
                        StringBuilder JSalesrecords = new StringBuilder();

                        //transforming into json

                        var JInventorySb = new InventoryManagementService(JInventoryrecords);
                        JInventorySb.ProcessProductsFromCsv(Config.PathToInvetoryFile);

                        var JSalesSb = new InventoryManagementService(JSalesrecords);
                        JSalesSb.ProcesOrdersFromCsv(Config.PathToSalesFile);


                        //making file out of json string

                        var JInventoryFile = new CsvToJsonConverter();
                        JInventoryFile.ProcessCsvFile(JInventoryrecords, Config.PathToData + "JInventoryFile.json");
                        var JSalesFile = new CsvToJsonConverter();
                        JSalesFile.ProcessCsvFile(JSalesrecords, Config.PathToData + "JSalesFile.json");
                    }
                    catch (Exception ex)
                    {
                        var Err = new CreateLogFiles();
                        Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
                        Console.WriteLine("Fatal error : " + ex.Message + ", please find a complete error at ErrorLog file");
                        throw;
                    }
                    Console.WriteLine("Updated database succesfully");
                }
                else if (input.ToLower() == "sql")
                {
                    try
                    {
                        var dropData = new InventoryManagementService();
                        dropData.DropAllData();

                        var CsvInvetoryRecords = new InventoryManagementService();
                        CsvInvetoryRecords.SaveCsvInventory(Config.PathToInvetoryFile);

                        var CsvSalesRecords = new InventoryManagementService();
                        CsvSalesRecords.SaveCsvOrders(Config.PathToSalesFile);


                    }
                    catch (Exception ex)
                    {
                        var Err = new CreateLogFiles();
                        Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
                        Console.WriteLine("Fatal error : " + ex.Message + ", please find a complete error at ErrorLog file");
                        throw;
                    }
                    Console.WriteLine("Updated database succesfully");
                }

            }
            else if(input == "retrieve")
            {
                StringBuilder records = new StringBuilder();
                Console.WriteLine(DisplayDbData.displayAllData(records).ToString()); 
            }



            //create inventory table


            //try
            //{
            //    using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
            //    {
            //        sqlConnection.Open();
            //        Console.WriteLine("Connection to database succesful");
            //        var sql = "CREATE TABLE Inventory (productId INTEGER CONSTRAINT PKeyMyId PRIMARY KEY," +
            //        "[Game Title] varchar(50), Category varchar(50), Platform varchar(50), [Available Units] int," +
            //        "Cost money, Price money, Condition varchar(50), [Product Type] varchar(50))";
            //        var cmd = new SqlCommand(sql, sqlConnection);
            //        cmd.ExecuteNonQuery();
            //    }

            //}
            //catch (SqlException ex)
            //{
            //    var Err = new CreateLogFiles();
            //    Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
            //    Console.WriteLine("Fatal error : " + ex.Message + ", please find a complete error at ErrorLog file");
            //    throw;
            //}

            ////create sales table
            ///
            //try
            //{
            //    using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
            //    {
            //        sqlConnection.Open();
            //        Console.WriteLine("Connection to database succesful");
            //        var sql = "CREATE TABLE Sales (orderId INTEGER CONSTRAINT PsalesKey PRIMARY KEY," +
            //            "Product varchar(50), Condition varchar(50), Date date, Total money, [Customer Name] varchar(50), [Customer Phone] varchar(30), Email varchar(50))";
            //        var cmd = new SqlCommand(sql, sqlConnection);
            //        cmd.ExecuteNonQuery();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    var Err = new CreateLogFiles();
            //    Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
            //    Console.WriteLine("Fatal error : " + ex.Message + ", please find a complete error at ErrorLog file");
            //    throw;
            //}

        }


    }
    
}