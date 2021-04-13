using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideogameShop.Library.Models
{
    public class UserRoleModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsSelected { get; set; }
    }
}
