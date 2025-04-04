using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IVendorAccessor
    {
        int DeleteVendor(int UserID, int VendorID);
        List<Vendor> GetAllVendors();
        int UpdateVendor(int VendorID, string VendorName, int AccountOwner, string ContactPerson, string PhoneNumber, string Email, int LeadTimeDays);
        Vendor GetItemVendor(int ItemID);
        int AddVendor(string VendorName, int AccountOwner, string ContactPerson, string PhoneNumber, string Email, int LeadTimeDays);
        Vendor GetVendorByID(int VendorID);
        Vendor GetVendorByName(string VendorName);
        List<string> GetVendorNames();
    }
}
