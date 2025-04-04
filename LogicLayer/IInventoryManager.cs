using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IInventoryManager
    {
        List<InventoryItem> GetInventoryItems();
        bool UpdateInventoryItem(int itemID, InventoryItem updateItem);
        bool AddInventoryItem(InventoryItem newItem, ItemLocation newLocation);
        bool RemoveInventoryItem(int itemID);
        bool AddInventoryLocation(int ItemID, ItemLocation location);
        bool UpdateInventoryLocation(int ItemID, ItemLocation location);
        ItemLocation GetLocationByItemID(int ItemID);
    }
}
