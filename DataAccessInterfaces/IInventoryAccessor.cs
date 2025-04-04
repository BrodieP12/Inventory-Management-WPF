using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IInventoryAccessor
    {
        int UpdateInventoryItem(
            int ItemID,
            string ItemName,
            int VendorID,
            string Description,
            decimal UnitPrice,
            int ReorderLevel,
            int WarrantyPeriodMonths,
            string SerialNumber,
            string Status,
            decimal ItemWeight,
            int QuantityOnHand,
            bool Active);
        int AddInventoryItem(
           string ItemName,
           int VendorID,
           string Description,
           decimal UnitPrice,
           int ReorderLevel,
           int WarrantyPeriodMonths,
           string SerialNumber,
           string Status,
           decimal ItemWeight,
           int QuantityOnHand,
           string LocationGUID,
           string LocationName);
        InventoryItem GetInventoryItem(int itemID);
        List<InventoryItem> GetInventoryItems();
        int DeleteInventoryItem(int itemID);
        int AddStockLevel(int userID, int itemID, int quantityOnHand);
        int UpdateStockLevel(int userID, int stockID, int quantityOnHand);
        List<InventoryReport> GenerateInventoryReport();

    }
}
