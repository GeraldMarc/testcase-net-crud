using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestCaseNetCrud.Models
{
    public class PurchaseOrdersEntity
    {
        public string ID { get; set; }
        public string Code { get; set; }
        [DisplayName("Purchase Date")]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }
        public string SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Remarks { get; set; }
    }
}