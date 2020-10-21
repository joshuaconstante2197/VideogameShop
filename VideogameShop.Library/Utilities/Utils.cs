using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace VideogameShop.Library.Utilities
{
    public class Utils
    {
        public static bool IfUserAuthenticated(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                return true;
            }
            return false;
        }

        public static bool IfUserInRole(string roleName, HttpContext httpContext)
        {
            if (IfUserAuthenticated(httpContext))
            {
                if (httpContext.User.IsInRole(roleName))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
