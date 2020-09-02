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

namespace VideogameShop.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class OrderController : Controller
    {
        
        // GET: OrderController
        public ActionResult Index()
        {
            DataTable dtblOrder = new DataTable();
            using(SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Sales", sqlCon);
                sqlDa.Fill(dtblOrder);
            }
            return View(dtblOrder);
        }

     
        // GET: OrderController/Create
        public ActionResult Upload()
        {
            var uploadOrder = new InventoryManagementService();
            uploadOrder.SaveCsvOrders(Config.PathToSalesFile);
            return RedirectToAction(nameof(Index));
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            return RedirectToAction(nameof(Index));
            
        }

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
