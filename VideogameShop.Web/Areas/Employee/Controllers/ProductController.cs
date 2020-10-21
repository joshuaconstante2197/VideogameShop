using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideogameShop.Library.Services;
using VideogameShopLibrary;
using VideogameShopLibrary.CVS_Models;
using VideogameShopLibrary.Services;
using System.IO;
using VideogameShop.Library.Utilities;

namespace VideogameShop.Web.Controllers
{
    [Area("Employee")]
    public class ProductController : Controller
    {
        
        // GET: ProductController


        [HttpGet]
        public ActionResult Index()
        {
            var Products = DisplayDbData.DisplayInventory(new List<Product>());
            ViewBag.IsAuthorized = Utils.IfUserAuthenticated(HttpContext);
            if(TempData["rowsAffected"] != null)
            {
                ViewBag.Message = TempData["rowsAffected"].ToString();
            }

            return View(Products);
        }
        public void getContext(HttpContext context)
        {
            var host = $"{context.Request.Scheme}://{context.Request.Host}";

        }

        //ProductController/Upload
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> UploadAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
                try
                {
                    var filePath = Path.GetTempFileName();
                    var uploadProduct = new InventoryManagementService();
                    int rowsAffected = 0;
                    FileStream stream = null;

                    using (stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    uploadProduct.SaveProductChar(stream.Name);
                    rowsAffected = uploadProduct.SaveCsvInventory(stream.Name);

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


        // GET: ProductController/Create
        public ActionResult Create()
        {
            //passing all the product characteristics to view to create dropdown menus
            ProductCharacteristics productCharacteristics = new ProductCharacteristics();
            DisplayDbData.DisplayProductCharacteristics(productCharacteristics);
            ViewBag.ProductCharacteristics = productCharacteristics;
            return View(new Product());
        }
        

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            var Insert = new InventoryManagementService();

            if(!Insert.InsertNewProduct(product))
            {
                ViewBag.Message = "Failed to insert Product";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: ProductController/Edit/
        public ActionResult Edit(int Id)
        {
            try
            {
                var product = DisplayDbData.GetProductById(new Product(), Id);
                ProductCharacteristics productCharacteristics = new ProductCharacteristics();
                DisplayDbData.DisplayProductCharacteristics(productCharacteristics);
                ViewBag.ProductCharacteristics = productCharacteristics;
                return View(product);
            }
            catch (Exception)
            {
                ViewBag.Message = "Product not found";
                return RedirectToAction(nameof(Index));
                throw;
            }
            
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {

            var Update = new InventoryManagementService();

            if (!Update.UpdateProductById(product))
            {
                ViewBag.Message = "Update Unsusccesfull";
                return RedirectToAction(nameof(Index));

            }

            return RedirectToAction(nameof(Index));

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
