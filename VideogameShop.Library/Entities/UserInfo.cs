using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using VideogameShop.Library.Utilities;

namespace VideogameShop.Library.Entities
{
    public class UserInfo : IUser<string>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EnumUserStatus Status { get; set; }
    }
}
