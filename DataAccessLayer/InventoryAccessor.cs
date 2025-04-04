using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class InventoryAccessor : IInventoryAccessor
    {
        public int AddStockLevel(int userID, int itemID, int quantityOnHand)
        {
            throw new NotImplementedException();
        }

        public int DeleteInventoryItem(int itemID)
        {
            var result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_delete_inventory_item", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            
            cmd.Parameters["@ItemID"].Value = itemID;
            
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public List<InventoryReport> GenerateInventoryReport()
        {
            throw new NotImplementedException();
        }

        public InventoryItem GetInventoryItem(int itemID)
        {
            throw new NotImplementedException();
        }
        public List<InventoryItem> GetInventoryItems()
        {
            List<InventoryItem> items = new List<InventoryItem>();

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_list_inventory_item", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new InventoryItem()
                    {
                        ItemID = reader.GetInt32(0),
                        ItemName = reader.GetString(1),
                        VendorID = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        UnitPrice = (decimal)reader.GetDecimal(4),
                        ReorderLevel = reader.GetInt32(5),
                        WarrentyPeriodMonths = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                        SerialNumber = reader.IsDBNull(7) ? "N/A" : reader.GetString(7),
                        Status = reader.GetString(8),
                        ItemWeight = reader.IsDBNull(9) ? null : (decimal)reader.GetDecimal(9),
                        QuantityOnHand = reader.GetInt32(10),
                        DateAdded = reader.GetDateTime(11),
                        LastUpdated = reader.GetDateTime(12),
                        Active = reader.GetBoolean(13)
                    });
                }
                return AddLocationsToItems(AddVendorsToItems(items));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<InventoryItem> AddVendorsToItems(List<InventoryItem> items)
        {
            VendorAccessor vendorAccessor = new VendorAccessor();
            try
            {
                List<Vendor> vendors = vendorAccessor.GetAllVendors();
                foreach (InventoryItem item in items)
                {
                    foreach (Vendor vendor in vendors)
                    {
                        if (vendor.VendorID == item.VendorID)
                        {
                            item.Vendor = vendor;
                        }
                    }
                }
            } catch (Exception ex) {
                throw;
            }
            return items;
        }
        public List<InventoryItem> AddLocationsToItems(List<InventoryItem> items)
        {
            LocationAccessor locationAccessor = new LocationAccessor();
            try
            {
                List<ItemLocation> locations = locationAccessor.GetAllLocations();
                foreach (InventoryItem item in items)
                {
                    foreach (ItemLocation location in locations)
                    {
                        if (location.ItemID == item.ItemID)
                        {
                            item.Location = location;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return items;
        }

        public int UpdateInventoryItem(
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
            bool Active)
        {
            var result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_inventory_item", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@VendorID", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1);
            var unitPriceNewParam = cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
            unitPriceNewParam.Precision = 18;
            unitPriceNewParam.Scale = 2;
            cmd.Parameters.Add("@ReorderLevel", SqlDbType.Int);
            cmd.Parameters.Add("@WarrantyPeriodMonths", SqlDbType.Int);
            cmd.Parameters.Add("@SerialNumber", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ItemWeight", SqlDbType.Decimal);
            cmd.Parameters.Add("@QuantityOnHand", SqlDbType.Int);
            cmd.Parameters.Add("@Active", SqlDbType.Bit);

            cmd.Parameters["@ItemID"].Value = ItemID;
            cmd.Parameters["@ItemName"].Value = ItemName;
            cmd.Parameters["@VendorID"].Value = VendorID;
            cmd.Parameters["@Description"].Value = Description;
            cmd.Parameters["@UnitPrice"].Value = UnitPrice;
            cmd.Parameters["@ReorderLevel"].Value = ReorderLevel;
            cmd.Parameters["@WarrantyPeriodMonths"].Value = (int)WarrantyPeriodMonths;
            cmd.Parameters["@SerialNumber"].Value = (string)SerialNumber;
            cmd.Parameters["@Status"].Value = Status;
            cmd.Parameters["@ItemWeight"].Value = (decimal)ItemWeight;
            cmd.Parameters["@QuantityOnHand"].Value = QuantityOnHand;
            cmd.Parameters["@Active"].Value = Active;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public int UpdateStockLevel(int userID, int stockID, int quantityOnHand)
        {
            throw new NotImplementedException();
        }

        public int AddInventoryItem(string ItemName, int VendorID, string Description, decimal UnitPrice, int ReorderLevel, int WarrantyPeriodMonths, string SerialNumber, string Status, decimal ItemWeight, int QuantityOnHand, string LocationGUID, string LocationName)
        {
            var result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_add_inventory_item_with_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@ItemName", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@VendorID", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1);
            var unitPriceNewParam = cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
            unitPriceNewParam.Precision = 18;
            unitPriceNewParam.Scale = 2;
            cmd.Parameters.Add("@ReorderLevel", SqlDbType.Int);
            cmd.Parameters.Add("@WarrantyPeriodMonths", SqlDbType.Int);
            cmd.Parameters.Add("@SerialNumber", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ItemWeight", SqlDbType.Decimal);
            cmd.Parameters.Add("@QuantityOnHand", SqlDbType.Int);
            cmd.Parameters.Add("@LocationGUID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@LocationName", SqlDbType.NVarChar, 100);

            cmd.Parameters["@ItemName"].Value = ItemName;
            cmd.Parameters["@VendorID"].Value = VendorID;
            cmd.Parameters["@Description"].Value = Description;
            cmd.Parameters["@UnitPrice"].Value = UnitPrice;
            cmd.Parameters["@ReorderLevel"].Value = ReorderLevel;
            cmd.Parameters["@WarrantyPeriodMonths"].Value = (int)WarrantyPeriodMonths;
            cmd.Parameters["@SerialNumber"].Value = (string)SerialNumber;
            cmd.Parameters["@Status"].Value = Status;
            cmd.Parameters["@ItemWeight"].Value = (decimal)ItemWeight;
            cmd.Parameters["@QuantityOnHand"].Value = QuantityOnHand;
            cmd.Parameters["@LocationGUID"].Value = LocationGUID;
            cmd.Parameters["@LocationName"].Value = LocationName;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
