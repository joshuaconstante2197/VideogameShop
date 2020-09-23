using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideogameShop.Library.Services;
using VideogameShopLibrary;
using VideogameShopLibrary.CVS_Models;
using VideogameShopLibrary.Services;

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
            return View(Products);
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
