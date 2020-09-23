using ChoETL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VideogameShopLibrary.CVS_Models
{
    [ChoCSVFileHeader]
    public class Product
    {
        [Key]
        [ChoCSVRecordField(1)]
        public int productId { get; set; }

        [ChoCSVRecordField(2)]
        public string GameTitle { get; set; }


        [ChoCSVRecordField(3)]
        public string Category { get; set; }

        [ChoCSVRecordField(4)]
        public string Platform { get; set; }


        [ChoCSVRecordField(5, FieldName = "Available Units")]
        [ChoTypeConverter(typeof(Int16Converter))]
        public int AvailableUnits { get; set; }


        [ChoCSVRecordField(6)]
        public decimal Cost { get; set; }


        [ChoCSVRecordField(7)]
        public decimal Price { get; set; }

        [ChoCSVRecordField(8)]
        public string Condition { get; set; }
        [ChoCSVRecordField(9, FieldName = "Product Type")]
        public string ProductType { get; set; }

    }
}
