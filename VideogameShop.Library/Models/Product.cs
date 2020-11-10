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
        public int productId { get; set; }

        [ChoCSVRecordField(1, FieldName = "Game Title")]
        public string GameTitle { get; set; }


        [ChoCSVRecordField(2)]
        public string Category { get; set; }

        [ChoCSVRecordField(3)]
        public string Platform { get; set; }


        [ChoCSVRecordField(4, FieldName = "Available Units")]
        [ChoTypeConverter(typeof(Int16Converter))]
        public int AvailableUnits { get; set; }


        [ChoCSVRecordField(5)]
        public decimal Cost { get; set; }


        [ChoCSVRecordField(6)]
        public decimal Price { get; set; }

        [ChoCSVRecordField(7)]
        public string Condition { get; set; }
        [ChoCSVRecordField(8, FieldName = "Product Type")]
        public string ProductType { get; set; }

    }
}
