using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class InventoryManager : IInventoryManager
    {
        private IInventoryAccessor _inventoryAccessor;
        private IItemLocationAccessor _itemLocationAccessor;
        public InventoryManager()
        {
            _inventoryAccessor = new InventoryAccessor();
            _itemLocationAccessor = new LocationAccessor();
        }
        public InventoryManager(IInventoryAccessor inventoryAccessor, IItemLocationAccessor locationAccessor)
        {
            _inventoryAccessor = inventoryAccessor;
            _itemLocationAccessor = locationAccessor;
        }
        public List<InventoryItem> GetInventoryItems()
        {
            List<InventoryItem> items = new List<InventoryItem>();
            try
            {
                items = _inventoryAccessor.GetInventoryItems();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Item retreival failed", ex);
            }

            return items;
        }

        public bool UpdateInventoryItem(int itemID, InventoryItem updateItem)
        {

            bool result = false;
            try
            {
                result = (0 == _inventoryAccessor.UpdateInventoryItem(itemID, updateItem.ItemName, updateItem.VendorID, updateItem.Description, updateItem.UnitPrice, updateItem.ReorderLevel,
                (int)updateItem.WarrentyPeriodMonths, (string)updateItem.SerialNumber, updateItem.Status, (decimal)updateItem.ItemWeight,
                updateItem.QuantityOnHand, updateItem.Active));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed.", ex);
            }
            return result;

        }
        public bool AddInventoryItem(InventoryItem newItem, ItemLocation newLocation)
        {
            bool result = false;
            result = (1 == _inventoryAccessor.AddInventoryItem(
                newItem.ItemName,
                newItem.VendorID,
                newItem.Description,
                newItem.UnitPrice,
                newItem.ReorderLevel,
                (int)newItem.WarrentyPeriodMonths,
                (string)newItem.SerialNumber,
                newItem.Status,
                (decimal)newItem.ItemWeight,
                newItem.QuantityOnHand,
                newLocation.LocationGUID,
                newLocation.LocationName));
            return result;
        }

        public bool RemoveInventoryItem(int itemID)
        {
            bool result = false;
            result = (1 == _inventoryAccessor.DeleteInventoryItem(itemID));
            return result;
        }

        public bool AddInventoryLocation(int ItemID, ItemLocation location)
        {
            bool result = false;
            result = (1 == _itemLocationAccessor.AdditemLocation(ItemID, location.LocationGUID, location.LocationName));
            return result;
        }

        public bool UpdateInventoryLocation(int ItemID, ItemLocation location)
        {
            bool result = false;
            result = (1 == _itemLocationAccessor.UpdateItemLocation(ItemID, location.LocationGUID, location.LocationName));
            return result;
        }

        public ItemLocation GetLocationByItemID(int ItemID)
        {
            ItemLocation location = new ItemLocation();
            try
            {
                location = _itemLocationAccessor.GetItemLocation(ItemID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Item retreival failed", ex);
            }
            return location;
        }
    }
}
