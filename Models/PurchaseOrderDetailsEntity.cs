namespace TestCaseNetCrud.Models
{
    public class PurchaseOrderDetailsEntity
    {
        public string ID { get; set; }
        public string PurchaseOrderID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}