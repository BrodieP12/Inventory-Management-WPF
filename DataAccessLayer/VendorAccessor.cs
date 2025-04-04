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
    public class VendorAccessor : IVendorAccessor
    {
        public int AddVendor(string VendorName, int AccountOwner, string ContactPerson, string PhoneNumber, string Email, int LeadTimeDays)
        {
            int result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_add_vendor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VendorName", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@AccountOwner", SqlDbType.Int);
            cmd.Parameters.Add("@ContactPerson", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@LeadTimeDays", SqlDbType.Int);

            cmd.Parameters["@VendorName"].Value = VendorName;
            cmd.Parameters["@AccountOwner"].Value = AccountOwner;
            cmd.Parameters["@ContactPerson"].Value = ContactPerson;
            cmd.Parameters["@PhoneNumber"].Value = PhoneNumber;
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters["@LeadTimeDays"].Value = LeadTimeDays;

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

        public int DeleteVendor(int userID, int vendorID)
        {
            int rowCount = 0;
            return 0;
        }

        public List<Vendor> GetAllVendors()
        {
            List<Vendor> vendors = new List<Vendor>();

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_get_all_vendors", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    vendors.Add(new Vendor()
                    {
                        VendorID = reader.GetInt32(0),
                        VendorName = reader.GetString(1),
                        AccountOwner = reader.GetInt32(2),
                        ContactPerson = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Email = reader.GetString(5),
                        LeadTimeDays = reader.GetInt32(6)
                    });
                }
                return vendors;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Vendor GetItemVendor(int ItemID)
        {
            Vendor vendor = null;


            return vendor;
        }

        public Vendor GetVendorByID(int VendorID)
        {
            Vendor vendor = null;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_vendor_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VendorID", VendorID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    vendor = new Vendor()
                    {
                        VendorID = reader.GetInt32(0),
                        VendorName = reader.GetString(1),
                        AccountOwner = reader.GetInt32(2),
                        ContactPerson = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Email = reader.GetString(5),
                        LeadTimeDays = reader.GetInt32(6)
                    };
                    return vendor;
                }
                else
                {
                    throw new ApplicationException("Vendor record not found");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Vendor GetVendorByName(string VendorName)
        {
            Vendor vendor = null;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_get_vendor_by_vendor_name", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VendorName", VendorName);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    vendor = new Vendor()
                    {
                        VendorID = reader.GetInt32(0),
                        VendorName = reader.GetString(1),
                        AccountOwner = reader.GetInt32(2),
                        ContactPerson = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Email = reader.GetString(5),
                        LeadTimeDays = reader.GetInt32(6)
                    };
                    return vendor;
                }
                else
                {
                    throw new ApplicationException("Vendor record not found");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public List<string> GetVendorNames()
        {
            List<string> vendorNames = new List<string>();

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_get_all_vendor_names", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    vendorNames.Add(reader.GetString(0));
                }
                return vendorNames;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int UpdateVendor(int VendorID, string VendorName, int AccountOwner, string ContactPerson, string PhoneNumber, string Email, int LeadTimeDays)
        {
            int result = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_vendor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VendorID", SqlDbType.Int);
            cmd.Parameters.Add("@VendorName", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@AccountOwner", SqlDbType.Int);
            cmd.Parameters.Add("@ContactPerson", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@LeadTimeDays", SqlDbType.Int);

            cmd.Parameters["@VendorID"].Value = VendorID;
            cmd.Parameters["@VendorName"].Value = VendorName;
            cmd.Parameters["@AccountOwner"].Value = AccountOwner;
            cmd.Parameters["@ContactPerson"].Value = ContactPerson;
            cmd.Parameters["@PhoneNumber"].Value = PhoneNumber;
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters["@LeadTimeDays"].Value = LeadTimeDays;

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
