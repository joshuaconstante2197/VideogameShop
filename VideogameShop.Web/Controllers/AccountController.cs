﻿using System;
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
        public async Task<IActionResult> Register(AppUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName };
                var resultUser = await userManager.CreateAsync(user, model.Password);
                IdentityResult resultRole; 
                if (resultUser.Succeeded)
                {
                    var role = await roleManager.FindByNameAsync(model.Role);

                    if (role != null)
                    {
                        resultRole = await userManager.AddToRoleAsync(user, model.Role);

                        if (resultRole.Succeeded)
                        {
                            await signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Index", "Home");
                        }

                        foreach (var error in resultRole.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }
                }

                foreach (var error in resultUser.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}