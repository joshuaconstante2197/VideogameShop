using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideogameShopLibrary;
using VideogameShopLibrary.CVS_Models;
using VideogameShop.Library.Services;
using System.Reflection;

namespace VideogameShop.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class OrderController : Controller
    {
        
        // GET: OrderController
        public ActionResult Index()
        {
            //DataTable dtblOrder = new DataTable();
            //using(SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
            //{
            //    sqlCon.Open();
            //    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Sales", sqlCon);
            //    sqlDa.Fill(dtblOrder);
            //}
            //return View(dtblOrder);
            List<Order> orders = new List<Order>();
            using (SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Sales", sqlCon);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            PropertyInfo propertyInfo = order.GetType().GetProperty(reader.GetName(i).Trim());

                            if (propertyInfo != null)
                            {
                                propertyInfo.SetValue(order, reader.GetValue(i), null);
                            }
                        }
                        orders.Add(order);
                    }
                }

            }
            return View(orders);
            
        }

        public ActionResult IndexFilteredByDate(DateTime fromDate, DateTime toDate)
        {
            List<Order> orders = new List<Order>();
            using(SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
            {
                sqlCon.Open();
                var sql = $"SELECT * FROM Sales WHERE (Date >= {fromDate} AND <= {toDate})";
                SqlCommand cmd = new SqlCommand(sql,sqlCon);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Order order = new Order();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            PropertyInfo propertyInfo = order.GetType().GetProperty(reader.GetName(i));

                            if (propertyInfo != null)
                            {
                                propertyInfo.SetValue(order, reader.GetValue(i), null);
                            }
                        }
                        orders.Add(order);
                    }
                }

            }
            return View(orders);
        }

     
        // GET: OrderController/Upload
        public ActionResult Upload()
        {
            var uploadOrder = new InventoryManagementService();
            uploadOrder.SaveCsvOrders(Config.PathToSalesFile);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Create()
        {
            return View(new Order());
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (order.TypeOfSale == "Credit")
            {
                CreditCardValidationService validateCard = new CreditCardValidationService();
                
                if(validateCard.Validate(order.CreditCardNumber))
                {
                    using (SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
                    {
                        sqlCon.Open();
                        var sql = "INSERT INTO Sales(Product, Quantity, Condition, Date, Total, [Customer Name], [Customer Phone] ,Email, [Type of Sale]," +
                                   "[Name on Credit Card], [Credit Card Number], [Expiration Date], [Security Code])" +
                                  $"VALUES ('{order.Product}', {order.Quantity}, '{order.Condition}', '{order.Date}', {order.Total}," +
                                  $"'{order.CustomerName}', '{order.CustomerPhone}', '{order.Email}','{order.TypeOfSale}'," +
                                  $"'{order.CreditCardName}', {order.CreditCardNumber}, '{order.ExpirationDate}', {order.SecurityCode})";
                        SqlCommand cmd = new SqlCommand(sql, sqlCon);
                        cmd.ExecuteNonQuery();
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Invalid Credit Card";
                    return View();
                    
                }
            }
            else if(order.TypeOfSale == "Cash")
            {
                using (SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
                {
                    sqlCon.Open();
                    var sql = "INSERT INTO Sales(Product, Quantity, Condition, Date, Total, [Customer Name], [Customer Phone], Email, [Type of Sale])" +
                   $"VALUES('{order.Product}', {order.Quantity}, '{order.Condition}', '{order.Date}', {order.Total}, '{order.CustomerName}', '{order.CustomerPhone}'," +
                   $"'{order.Email}','{order.TypeOfSale}')";
                    SqlCommand cmd = new SqlCommand(sql, sqlCon);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Invalid Type of Sale');</script>");
            }

        }
        //to place order directly from a product
        public ActionResult CreateFromProduct(int Id)
        {
            Product product = new Product();
            List<string> p = new List<string>();
            using (SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT [Game Title],Condition, Price FROM Inventory WHERE productId = @productId", sqlCon);
                cmd.Parameters.AddWithValue("@productId", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        { 
                            product.GameTitle = reader.GetValue(reader.GetOrdinal("Game Title")).ToString();
                            product.Condition = reader.GetValue(reader.GetOrdinal("Condition")).ToString();
                            product.Price = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Price")));
                        }
                    }
            }
            ViewBag.Product = product;
            return View();

        }

        public PartialViewResult CreditCardPartial()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       


        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
