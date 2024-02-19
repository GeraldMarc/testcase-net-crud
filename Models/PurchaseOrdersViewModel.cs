using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCaseNetCrud.Models
{
    public class PurchaseOrdersViewModel
    {
        public PurchaseOrdersEntity PurchaseOrders { get; set; }
        public List<PurchaseOrderDetailsEntity> PurchaseOrderDetails { get; set; }
    }
}