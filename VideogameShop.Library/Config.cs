using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace VideogameShopLibrary
{
    public static class Config
    {
        public static string PathToData = @"..\VideogameShop.Library\Data\";
        public static string PathToInvetoryFile = @"..\Data\Videogame shop - Inventory.csv";
        public static string PathToSalesFile = @"..\VideogameShop.Library\Data\Videogame shop - Sales1.csv";


        //public static string PathToData = @"C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Library\Data\";
        //public static string PathToInvetoryFile = @"C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Library\Data\Videogame shop - Inventory.csv";
        //public static string PathToSalesFile = @"C:\Users\joshu\source\repos\joshuaconstante2197\Videogame-Shopv\VideogameShop.Library\Data\Videogame shop - Sales1.csv";

        public static string ConnString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
        public static string Key = "5843&1jahglaQPOKQ784521.,{Qjd~Hj";
    }
}
