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
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace VideogameShop.Web.Areas.Employee.Controllers
{
    
    public class OrderController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
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
        public async System.Threading.Tasks.Task<ActionResult> UploadAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
                try
                {
                    var filePath = Path.GetTempFileName();
                    var uploadOrder = new InventoryManagementService();
                    int rowsAffected = 0;
                    FileStream stream = null;
                    using (stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    rowsAffected = uploadOrder.SaveCsvOrders(stream.Name);
                    if (rowsAffected > 0)
                    {
                        TempData["rowsAffected"] = rowsAffected == 1 ? "1 row was affected" : $"{rowsAffected} rows were affected";
                    }
                    else
                    {
                        TempData["rowsAffected"] = "0 rows were affected";
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    var Err = new CreateLogFiles();
                    Err.ErrorLog(Config.PathToData + "err.log", ex.Message);
                    Console.WriteLine("Fatal error : " + ex.Message + ", please find a complete error at ErrorLog file");
                    throw;
                }
            else
            {
                ViewBag.Message = "Unable to upload file";
                return RedirectToAction(nameof(Index));
            }

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
            DisplayDbData.GetProductById(product, Id);
            ViewBag.Product = product;
            return View();

        }

        public PartialViewResult CreditCardPartial()
        {
            return PartialView();
        }



        
       

        
    }
}
