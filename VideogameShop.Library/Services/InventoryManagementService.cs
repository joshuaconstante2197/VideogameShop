using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ChoETL;
using VideogameShop.Library.Services;
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
                          $"INSERT INTO P_Types(ProductType) SELECT('{productChar.ProductType}') WHERE NOT EXISTS(SELECT * FROM P_Types WHERE ProductType = '{productChar.ProductType}')";
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

        public int SaveCsvInventory(string inputFileName)
        {
            SqlCommand cmd;
            int rowsAffected = 0;

            foreach (var product in new ChoCSVReader<Product>(inputFileName)
                .WithFirstLineHeader()
                )
            {

                var sql = $"IF NOT EXISTS(SELECT * FROM Inventory WHERE GameTitle = '{product.GameTitle}')" +
                    "BEGIN " +
                        "INSERT INTO Inventory(GameTitle, Category, Platform, AvailableUnits, Cost, Price, Condition, ProductType)" +
                        $"VALUES('{product.GameTitle}', '{product.Category}',  '{product.Platform}', {product.AvailableUnits}," +
                        $"{product.Cost} , {product.Price},  '{product.Condition}',  '{product.ProductType}' ) " +
                    "END";
                using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand(sql, sqlConnection);
                    rowsAffected += cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine(rowsAffected + " rows were affected");
            return rowsAffected;
        }

        /// <summary>
        /// Saves Sales CSV data into database, removes 1 item from inventory where Game Title matches
        /// </summary>
        public void SaveCsvOrders(string inputFileName)
        {

            SqlCommand cmd;


            //insert each item to database
            foreach (var order in new ChoCSVReader<Order>(inputFileName)
                .WithFirstLineHeader()
                )
            {

                //inserts each item into database
                string sql;
                //check if its credit to insert credit card fields
                if(order.SaleType == "Credit")
                {
                    var validateCc = new CreditCardValidationService();
                    //check if credit card is valid to start insert
                    if(validateCc.Validate(order.CreditCardNumber))
                    {
                        //encrypt credit card
                        var ccEncrypted = CcOperation.Encrypt(Config.Key, order.CreditCardNumber);
                        //displayig only last 4 digits so save them as a string 
                        var lastFourDigits = order.CreditCardNumber.Substring(order.CreditCardNumber.Length - 4, 4);

                        sql = "INSERT INTO Sales(Product, Quantity, Condition, Date, Total, CustomerName, CustomerPhoneNumber ,Email, SaleType," +
                                   "CreditCardName, CreditCardNumber, EncryptedCreditCardNumber, ExpirationDate, SecurityCode)" +
                                  $"VALUES ('{order.Product}', {order.Quantity}, '{order.Condition}', '{order.Date}', {order.Total}," +
                                  $"'{order.CustomerName}', '{order.CustomerPhoneNumber}', '{order.Email}','{order.SaleType}'," +
                                  $"'{order.CreditCardName}', '{lastFourDigits}', '{ccEncrypted}', '{order.ExpirationDate}', {order.SecurityCode})";
                    }
                    else
                    {
                        continue;
                    }
                   
                }
                //if not credit then insert only order information
                else
                {
                     sql = "INSERT INTO Sales(Product, Quantity, Condition, Date, Total, CustomerName, CustomerPhoneNumber ,Email, SaleType)" +
                                                      $"VALUES ('{order.Product}', {order.Quantity}, '{order.Condition}', '{order.Date}', {order.Total}," +
                                                      $"'{order.CustomerName}', '{order.CustomerPhoneNumber}', '{order.Email}','{order.SaleType}')";
                }
                

                using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand(sql, sqlConnection);
                    cmd.ExecuteNonQuery();
                }

                //Removes 1 from inventory where Game Title matches 
                var sql2 = $"UPDATE Inventory SET AvailableUnits = AvailableUnits - 1 WHERE GameTitle = '{order.Product}'";

                using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                {
                    sqlConnection.Open();
                    cmd = new SqlCommand(sql2, sqlConnection);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //method to create a new product
        public bool InsertNewProduct(Product product)
        {
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();

                var sql = "INSERT INTO Inventory(GameTitle, Category, Platform, AvailableUnits, Cost , Price, Condition, ProductType)" +
                $"VALUES('{product.GameTitle}', '{product.Category}',  '{product.Platform}', {product.AvailableUnits}," +
                $"cast({product.Cost} as money),  {product.Price},  '{product.Condition}',  '{product.ProductType}' )";
                try
                {
                    SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
                    sqlCmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    var Err = new CreateLogFiles();
                    Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
                    return false;
                    throw;
                }
                
            }
        }
        public bool UpdateProductById(Product product )
        {
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                {
                    var sql = $"UPDATE Inventory SET (GameTitle = '{product.GameTitle}'," +
                            $"Category = '{product.Category}'," +
                            $"Platform = '{product.Platform}', " +
                            $"AvailableUnits = {product.AvailableUnits}, " +
                            $"Cost = {product.Cost}, " +
                            $"Price = {product.Price}, " +
                            $"Condition = '{product.Condition}', " +
                            $"ProductType = '{product.ProductType}'" +
                            $"WHERE productId = {product.productId})";
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
                        sqlCmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var Err = new CreateLogFiles();
                        Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
                        return false;
                        throw;
                    }

                }
            }
        }
        //method to add credit card orders
        public bool CreateCreditCardOrder(Order order)
        {
            CreditCardValidationService card = new CreditCardValidationService();

            if(card.Validate(order.CreditCardNumber))
            {
                var ccEncrypted = CcOperation.Encrypt(Config.Key, order.CreditCardNumber);
                var lastFourDigits = order.CreditCardNumber.Substring(order.CreditCardNumber.Length - 4, 4);

                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("spCreateCreditCardOrder", sqlCon);

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Product", order.Product));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", order.Quantity));
                        cmd.Parameters.Add(new SqlParameter("@Condition", order.Condition));
                        cmd.Parameters.Add(new SqlParameter("@Date", order.Date));
                        cmd.Parameters.Add(new SqlParameter("@Total", order.Total));
                        cmd.Parameters.Add(new SqlParameter("@CustomerName", order.CustomerName));
                        cmd.Parameters.Add(new SqlParameter("@CustomerPhoneNumber", order.CustomerPhoneNumber));
                        cmd.Parameters.Add(new SqlParameter("@Email", order.Email));
                        cmd.Parameters.Add(new SqlParameter("@SaleType", order.SaleType));
                        cmd.Parameters.Add(new SqlParameter("@CreditCardName", order.CreditCardName));
                        cmd.Parameters.Add(new SqlParameter("@CreditCardNumber", lastFourDigits));
                        cmd.Parameters.Add(new SqlParameter("@EncryptedCreditCardNumber", ccEncrypted));
                        cmd.Parameters.Add(new SqlParameter("@ExpirationDate", order.ExpirationDate));
                        cmd.Parameters.Add(new SqlParameter("@SecurityCode", order.SecurityCode));

                        cmd.ExecuteNonQuery();

                        var sql2 = $"UPDATE Inventory SET AvailableUnits = AvailableUnits - {order.Quantity} WHERE GameTitle = '{order.Product}'";

                        using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                        {
                            sqlConnection.Open();
                            cmd = new SqlCommand(sql2, sqlConnection);
                            cmd.ExecuteNonQuery();
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var Err = new CreateLogFiles();
                        Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
                        return false;
                        throw;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        //method to add cash orders
        public bool CreateCashOrder(Order order)
        {
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                try
                {
                    //selecting stored procedure and adding all parameters
                    SqlCommand cmd = new SqlCommand("spCreateCashOrder", sqlCon);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Product", order.Product));
                    cmd.Parameters.Add(new SqlParameter("@Quantity", order.Quantity));
                    cmd.Parameters.Add(new SqlParameter("@Condition", order.Condition));
                    cmd.Parameters.Add(new SqlParameter("@Date", order.Date));
                    cmd.Parameters.Add(new SqlParameter("@Total", order.Total));
                    cmd.Parameters.Add(new SqlParameter("@CustomerName", order.CustomerName));
                    cmd.Parameters.Add(new SqlParameter("@CustomerPhoneNumber", order.CustomerPhoneNumber));
                    cmd.Parameters.Add(new SqlParameter("@Email", order.Email));
                    cmd.Parameters.Add(new SqlParameter("@SaleType", order.SaleType));

                    cmd.ExecuteNonQuery();
                    //removing items from inventory
                    var sql2 = $"UPDATE Inventory SET AvailableUnits = AvailableUnits - {order.Quantity} WHERE GameTitle = '{order.Product}'";

                    using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
                    {
                        sqlConnection.Open();
                        cmd = new SqlCommand(sql2, sqlConnection);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    var Err = new CreateLogFiles();
                    Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
                    return false;
                    throw;
                }
            }
        }


        public void DropAllData()
        {
            SqlCommand cmd;
            var sql = "DELETE FROM Inventory; DELETE FROM Sales; DELETE FROM P_Categories; DELETE FROM P_Conditions; DELETE FROM P_Platforms; DELETE FROM P_Types;";
            int rows = 0;
            using (SqlConnection sqlConnection = new SqlConnection(Config.ConnString))
            {
                sqlConnection.Open();
                cmd = new SqlCommand(sql, sqlConnection);
                rows = cmd.ExecuteNonQuery();
            }
            Console.WriteLine("dropped al data");
            Console.WriteLine("Rows Affected " + rows);
        }

    }
}
