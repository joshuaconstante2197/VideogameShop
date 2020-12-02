using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideogameShop.Library.Models;
using VideogameShop.Library.Services.Authorization;
using VideogameShop.Web.Areas.Employee.ViewModels;

namespace VideogameShop.Web.Areas.Employee.Controllers
{
    
    public class AdministrationController : Controller
    {
        

        

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRole(Role role)
        {
            if (ModelState.IsValid)
            {
                var newRole = new ManageRoles();
                if(newRole.AddRole(role))
                {
                    return RedirectToAction("Administration","ListRoles");
                }
                else
                {
                    ViewBag.Message = "Add role attempt unsuscessfull, please try again or refer to the error log";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = new ManageRoles().GetRoles(new Role());
            return View(roles);
        }

        [HttpGet]
        public IActionResult EditRole(string id)
        {
            var roleToEdit = new ManageRoles();
            
            if (!roleToEdit.GetRoleById(id))
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("Error");
            }
           
            return View(roleToEdit);
        }
        [HttpPost]
        public IActionResult EditRole(Role role)
        {
            var editRole = new ManageRoles();
            if(editRole.EditRoleById(role))
            {
                return RedirectToAction("Administration", "ListRoles");
            }
            else
            {
                ViewBag.Message = "Edit role attempt unsuscessfull, please try again or refer to the error log";
                return View();
            }
        }

        //[HttpGet]
        //public IActionResult EditUserInRole(string roleId)
        //{
        //    ViewBag.roleId = roleId;

        //    var role = new ManageRoles().GetRoleById(roleId);
        //    if (role == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
        //        return View("Error");
        //    }

        //    var model = new List<AppUser>();

        //    foreach (var user in model)
        //    {
        //        var userRoleViewModel = new UserRoleViewModel
        //        {
        //            UserId = user.Id,
        //            UserName = user.UserName
        //        };
        //        if (await userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            userRoleViewModel.IsSelected = true;
        //        }
        //        else
        //        {
        //            userRoleViewModel.IsSelected = false;
        //        }

        //        model.Add(userRoleViewModel);
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string roleId)
        //{
        //    var role = await roleManager.FindByIdAsync(roleId);
        //    if (role == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
        //        return View("Error");
        //    }

        //    for (int i = 0; i < model.Count; i++)
        //    {
        //        var user = await userManager.FindByIdAsync(model[i].UserId);

        //        IdentityResult result = null;

        //        if(model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
        //        {
        //           result = await userManager.AddToRoleAsync(user, role.Name);
        //        }
        //        else if(!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            result = await userManager.RemoveFromRoleAsync(user, role.Name);
        //        }
        //        else
        //        {
        //            continue;
        //        }

        //        if(result.Succeeded)
        //        {
        //            if (i < (model.Count - 1))
        //                continue;
        //            else
        //                return RedirectToAction("EditRole", new { id = roleId });
        //        }
        //    }
        //    return RedirectToAction("EditRole", new { id = roleId });
        //}

    }
}