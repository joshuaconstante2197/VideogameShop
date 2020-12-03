using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideogameShop.Library.Models;
using VideogameShop.Library.Services.Authentication;
using VideogameShop.Library.Services.Authorization;

namespace VideogameShop.Web.Areas.Employee.Controllers
{
    

    public class AccountController : Controller
    {

        
        [HttpGet]
        public IActionResult Register()
        {
            var roles = new ManageRoles().GetRoles();
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
        public IActionResult Login(AppUser user)
        {
            if (ModelState.IsValid)
            {
                var loginUser = new ProcessAccountData();
                if (loginUser.Login(user))
                {
                    HttpContext.Session.SetString("UserName", user.UserName);
                    HttpContext.Session.SetString("Role", user.Role);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("All", "Invalid Email or Password");
                    return View();
                }
            }
            else return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
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

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}