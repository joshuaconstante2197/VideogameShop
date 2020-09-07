using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideogameShopLibrary;
using VideogameShopLibrary.CVS_Models;

namespace VideogameShop.Web.Controllers
{
    [Area("Employee")]
    public class ProductController : Controller
    {
        [HttpGet]
        // GET: ProductController
        public ActionResult Index()
        {
            //Todo: This Logic should be placed in th Class Library and only be invoked from here
            DataTable dtblProduct = new DataTable();
            using(SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Inventory", sqlCon);
                sqlDa.Fill(dtblProduct);
            }

            //Todo: Best practice, avoid dealing with datatables in the UI, work with collections instead e.g. List<Product>
            return View(dtblProduct);
        }


        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View(new Product());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            
               //Todo: This Logic should be placed in th Class Library and only be invoked from here
                int productId;
                string getMax = @"SELECT COALESCE(MAX(productId), 0) + 1 AS maxPlusOne FROM Inventory";
                using (SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(getMax, sqlCon))
                    {
                        sqlCon.Open();
                        productId = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    }
                }
                using (SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
                {
                    sqlCon.Open();

                    var sql = "INSERT INTO Inventory(productId, [Game Title], Category, Platform, [Available Units], Cost , Price, Condition, [Product Type])" +
                    $"VALUES({productId}, '{product.GameTitle}', '{product.Category}',  '{product.Platform}', {product.AvailableUnits}," +
                    $"cast({product.Cost} as money),  {product.Price},  '{product.Condition}',  '{product.ProductType}' )";
                    SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
                    sqlCmd.ExecuteNonQuery();

                }
                return RedirectToAction(nameof(Index));
            
            
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int Id)
        {
            
            Product product = new Product();

            //Todo: This Logic should be placed in th Class Library and only be invoked from here
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
            {
                sqlCon.Open();
                string cmd = $"SELECT * FROM Inventory Where productId = @productId";
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd,sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@productId", Id);
                sqlDa.Fill(dtblProduct);
            }
            if(dtblProduct.Rows.Count == 1)
            {
                //TODO: Avoid relying on indexes, use the Column Value name instead e.g. dtblProduct.Rows[0]["GameTitle"]
                product.GameTitle = dtblProduct.Rows[0][1].ToString();
                product.Category = dtblProduct.Rows[0][2].ToString();
                product.Platform = dtblProduct.Rows[0][3].ToString();
                product.AvailableUnits = Convert.ToInt32(dtblProduct.Rows[0][4].ToString());
                product.Cost = Convert.ToDecimal(dtblProduct.Rows[0][5].ToString());
                product.Price = Convert.ToDecimal(dtblProduct.Rows[0][6].ToString());
                product.Condition = dtblProduct.Rows[0][7].ToString();
                product.ProductType = dtblProduct.Rows[0][8].ToString();
                return View(product);

            }
            else
                return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
