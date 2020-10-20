using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideogameShop.Library.Models;

namespace VideogameShop.Web.Areas.Employee.Controllers
{
    public class UserController : Controller
    {
        [Area("Employee")]

        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            AppUser userModel = new AppUser();
            return View(userModel);
        }

        [HttpPost]
        public IActionResult AddOrEdit(AppUser appUser)
        {
            using (DbModels )
            {

            }
        }
    }
}
