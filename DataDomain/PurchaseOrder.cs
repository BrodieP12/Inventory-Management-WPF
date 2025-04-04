using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class PurchaseOrder
    {
        public int PurchaseOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string Status { get; set; }
        public double CostOfOrder { get; set; }
    }
    public class PurchaseOrderVM : PurchaseOrder
    {
        public Vendor Vendor { get; set; }
    }
}
