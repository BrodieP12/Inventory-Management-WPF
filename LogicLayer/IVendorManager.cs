using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IVendorManager
    {
        List<string> GetAllVendorNames();
        List<Vendor> GetAllVendors();
        Vendor GetVendorById(int VendorId);
        Vendor GetVendorByName(string Name);
        int DeleteVendor(int VendorID);
        bool UpdateVendor(int VendorID, Vendor Vendor);
        Vendor GetItemVendor(int ItemID);
        bool AddVendor(Vendor vendor);
    }
}
