using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VideogameShop.Library.Models
{
    public class AppUser
    {
        public int UserId { get; set; }

        [Required(ErrorMessage ="This field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }
    }
}
