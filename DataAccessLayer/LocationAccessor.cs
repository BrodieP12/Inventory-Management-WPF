using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LocationAccessor : IItemLocationAccessor
    {
        public int AdditemLocation(int ItemID, string LocationGUID, string LocationName)
        {
            var result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_add_item_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            cmd.Parameters.Add("@LocationGUID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@LocationName", SqlDbType.NVarChar, 100);
            
            cmd.Parameters["@ItemID"].Value = ItemID;
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
        public int UpdateItemLocation(int ItemID, string LocationGUID, string LocationName)
        {
            var result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_item_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            cmd.Parameters.Add("@LocationGUID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@LocationName", SqlDbType.NVarChar, 100);

            cmd.Parameters["@ItemID"].Value = ItemID;
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
        public List<ItemLocation> GetAllLocations()
        {
            List<ItemLocation> locations = new List<ItemLocation>();

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_get_all_item_locations", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    locations.Add(new ItemLocation()
                    {
                        ItemID = reader.GetInt32(0),
                        LocationGUID = reader.GetString(1),
                        LocationName = reader.GetString(2)
                    });
                }
                return locations;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ItemLocation GetItemLocation(int ItemID)
        {
            ItemLocation location = null;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_get_location_by_item_id", conn);

            cmd.Parameters.Add("@ItemID", SqlDbType.Int);

            cmd.Parameters["@ItemID"].Value = ItemID;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    location = new ItemLocation()
                    {
                        ItemID = ItemID,
                        LocationGUID = reader.GetString(0),
                        LocationName = reader.GetString(1)
                    };
                }
                return location;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
