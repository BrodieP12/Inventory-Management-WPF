using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class InventoryReport
    {
        public InventoryItem item; 
        public Vendor sendor;
    }

    public class InventoryReportVM : InventoryReport
    {
        public int ItemID;
        public string ItemName;
        public string VendorName;
        public int QuantityOnHand;
        public double UnitPrice;
        public int ReorderLevel;
        public bool Active;
    }
}
