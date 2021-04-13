using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideogameShop.Library.Models;
using VideogameShop.Library.Services.Authentication;
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
                if(!newRole.CheckIfRoleExist(role))
                {
                    if (newRole.AddRole(role))
                    {
                        return RedirectToAction("ListRoles", "Administration");
                    }
                    else
                    {
                        ModelState.AddModelError("All", "Add role attempt unsuscessfull, please try again or refer to the error log");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("All","Role already exists, please enter a new role.");
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = new ManageRoles().GetRoles();
            return View(roles);
        }

        [HttpGet]
        public IActionResult EditRole(int id)
        {
            var finder = new ManageRoles();
            var roleToEdit = finder.GetRoleById(id);
            
            if (roleToEdit == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("Error");
            }
            ViewBag.UsersInRole = finder.GetUsersInRole(id);
            return View(roleToEdit);
        }
        [HttpPost]
        public IActionResult EditRole(Role role)
        {
            var editRole = new ManageRoles();
            if(editRole.EditRoleById(role))
            {
                return RedirectToAction("ListRoles", "Administration");
            }
            else
            {
                ModelState.AddModelError("All", "Edit role unsusccesful");
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditUserInRole(int id)
        {

            var finder = new ManageRoles();
            var role = finder.GetRoleById(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("Error");
            }
            ViewBag.Role = role;

            var users = new UserManager().GetUsersByRole(role);

            return View(users);
        }

        [HttpPost]
        public IActionResult EditUserInRole(List<UserRoleModel> model, int id)
        {
            var role = new ManageRoles().GetRoleById(id);
            var result = new ManageRoles();
            
            if (role.RoleName == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("Error");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = new UserManager().GetUserById(model[i].UserId);

                if (!string.IsNullOrEmpty(user.Role))
                {
                    if (model[i].IsSelected && (user.Role != role.RoleName))
                    {
                        result.RemoveUserFromRole(user);
                        result.AddUserToRole(user, role);
                        continue;

                    }
                    else if (!model[i].IsSelected && (user.Role == role.RoleName))
                    {
                        result.RemoveUserFromRole(user);
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }

                else
                {
                    result.AddUserToRole(user, role);
                    continue;
                }

            }
            return RedirectToAction("EditRole", new { id = id });
        }

    }
}