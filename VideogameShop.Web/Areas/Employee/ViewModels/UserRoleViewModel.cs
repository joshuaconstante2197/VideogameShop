using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideogameShop.Web.Areas.Employee.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
