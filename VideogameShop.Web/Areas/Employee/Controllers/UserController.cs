using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideogameShop.Library.Models;

namespace VideogameShop.Web.Areas.Employee.Controllers
{
    public class UserController : Controller
    {
        [Area("Employee")]
        [HttpGet]
        public IActionResult AddorEdit(int id = 0)
        {
            var user = new AppUser();
            return View(user);
        }

        [HttpPost]
        public IActionResult AddorEdit(AppUser user)
        {

        }
    }
}
