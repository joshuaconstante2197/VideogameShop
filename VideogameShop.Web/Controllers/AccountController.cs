using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideogameShop.Library.Models;
using VideogameShop.Library.Services.Authentication;

namespace VideogameShop.Web.Areas.Employee.Controllers
{
    

    public class AccountController : Controller
    {

        
        [HttpGet]
        public IActionResult Register()
        {
            var roles = new ProcessAccountData().GetRoles();
            if(roles.Count > 0)
            {
                ViewBag.Roles = roles;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AppUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new ProcessAccountData();
                if(user.Register(model))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Order");
                }
            }
            return View();
        }
        



    }
}