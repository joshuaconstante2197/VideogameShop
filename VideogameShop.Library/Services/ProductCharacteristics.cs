using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using VideogameShopLibrary;
using VideogameShopLibrary.CVS_Models;

namespace VideogameShop.Library.Services
{
    //Object containing lists of the product categories necessary to hold the records from database
    public class ProductCharacteristics
    {
        public List<string> Category { get; private set; }
        public List<string> Condition { get; private set; }
        public List<string> Platform { get; private set; }
        public List<string> ProductType { get; private set; }

        public ProductCharacteristics()
        {
            Category = new List<string>();
            Condition = new List<string>();
            Platform = new List<string>();
            ProductType = new List<string>();
        }
    }
}
