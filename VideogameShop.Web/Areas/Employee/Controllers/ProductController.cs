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
        // GET: ProductController
        
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using(SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Inventory", sqlCon);
                sqlDa.Fill(dtblProduct);
            }
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
            
                using (SqlConnection sqlCon = new SqlConnection(Startup.GetConnectionString()))
                {
                    sqlCon.Open();

                    var sql = "INSERT INTO Inventory([Game Title], Category, Platform, [Available Units], Cost , Price, Condition, [Product Type])" +
                    $"VALUES('{product.GameTitle}', '{product.Category}',  '{product.Platform}', {product.AvailableUnits}," +
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
