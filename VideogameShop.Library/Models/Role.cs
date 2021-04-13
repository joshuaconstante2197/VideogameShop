using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VideogameShop.Library.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
