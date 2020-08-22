using ChoETL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VideogameShopLibrary.CVS_Models
{
    [ChoCSVFileHeader]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ChoCSVRecordField(1)]
        public string Product { get; set; }


        [ChoCSVRecordField(2)]
        public string Condition { get; set; }


        [ChoCSVRecordField(3)]
        [ChoTypeConverter(typeof(ChoDateTimeConverter))]
        
        public DateTime Date { get; set; }


        [ChoCSVRecordField(4)]
        public decimal Total { get; set; }

        [ChoCSVRecordField(5, FieldName = "Customer Name")]
        public string CustomerName { get; set; }


        [ChoCSVRecordField(6, FieldName = "Customer Phone")]
        public string CustomerPhone { get; set; }

        [ChoCSVRecordField(7)]
        public string Email { get; set; }



    }
}
