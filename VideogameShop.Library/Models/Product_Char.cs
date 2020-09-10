using System;
using System.Collections.Generic;
using System.Text;
using ChoETL;

namespace VideogameShopLibrary.CVS_Models
{   
    [ChoCSVFileHeader]
    public class Product_Char
    {
        [ChoCSVRecordField(2)]
        public string Category { get; set; }

        [ChoCSVRecordField(3)]
        public string Platform { get; set; }      

        [ChoCSVRecordField(7)]
        public string Condition { get; set; }

        [ChoCSVRecordField(8, FieldName = "Product Type")]
        public string ProductType { get; set; }
    }
}
