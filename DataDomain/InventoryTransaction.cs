using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class InventoryTransaction
    {
        public int TransactionID { get; set; }
        public int Quantity { get; set; }
        public string TransactionType { get; set; } // purchase, sale, adjustment, etc.
        public DateTime Timestamp { get; set; }
    }
    public class InventoryTransactionVM : InventoryTransaction
    {
        public PurchaseOrder? PurchaseOrder { get; set; }
    }
}
