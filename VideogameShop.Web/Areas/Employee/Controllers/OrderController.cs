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
using VideogameShopLibrary.Services;

namespace VideogameShop.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class OrderController : Controller
    {
        
        // GET: OrderController
        public ActionResult Index(DateTime fromDate, DateTime toDate)
        {
            string sql;
            
            //checking if the fromDate hasn't been initialized
            if (fromDate == Convert.ToDateTime("January 1, 0001"))
            {
                sql = "SELECT * FROM Sales";
            }

            else
            {
                //checking if fromDate is not higher than toDate
                if (fromDate > toDate)
                {
                    ViewBag.Message = "Invalid Date";
                    sql = "SELECT * FROM Sales";
                }
                else
                {
                    sql = $"SELECT * FROM Sales WHERE (Date >= '{fromDate}' AND Date <= '{toDate}')";
                }
            }
            List<Order> orders = DisplayDbData.DisplayOrders(new List<Order>(), sql);
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
            var insert = new InventoryManagementService();

            if (order.SaleType == "Credit")
            {

                if (insert.CreateCreditCardOrder(order))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Invalid Credit Card";
                    return View();
                }

            }
            
            else if(order.SaleType == "Cash")
            {
                if (insert.CreateCashOrder(order))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Problem Inserting order";
                    return View();
                }
            }

            else
            {
                ViewBag.Message = "Invalid type of sale";
                return View();
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
