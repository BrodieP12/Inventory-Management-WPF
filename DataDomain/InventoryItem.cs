using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class InventoryItem
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int VendorID { get; set; }
        public Vendor? Vendor { get; set; }
        public ItemLocation? Location { get; set; }
        public int QuantityOnHand { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int ReorderLevel { get; set; }
        public int? WarrentyPeriodMonths { get; set; }
        public string? SerialNumber { get; set; }
        public string Status { get; set; }
        public decimal? ItemWeight { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Active { get; set; }
    }
}
