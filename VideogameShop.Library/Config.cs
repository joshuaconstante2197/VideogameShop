using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace VideogameShopLibrary
{
    public static class Config
    {
        public static string PathToData = @"C:\Users\joshu\source\repos\joshuaconstante2197\VideogameShop.Library\Data\";
        public static string PathToInvetoryFile = @"C:\Users\joshu\source\repos\joshuaconstante2197\VideogameShop.Library\Data\Videogame shop - Inventory.csv";
        public static string PathToSalesFile = @"C:\Users\joshu\source\repos\joshuaconstante2197\VideogameShop.Library\Data\Videogame shop - Sales.csv";
        public static string ConnString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

    }
}
