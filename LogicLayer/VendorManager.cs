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
    public class VendorManager : IVendorManager
    {
        // pay attention to dependency inversion
        private IVendorAccessor _vendorAccessor;

        public VendorManager()
        {
            _vendorAccessor = new VendorAccessor();
        }
        public VendorManager(IVendorAccessor vendorAccessor)
        {
            _vendorAccessor = vendorAccessor;
        }
        public bool AddVendor(Vendor vendor)
        {
            bool result = false;
            result = (1 == _vendorAccessor.AddVendor(vendor.VendorName, vendor.AccountOwner, vendor.ContactPerson, vendor.PhoneNumber, vendor.Email, vendor.LeadTimeDays));

            return result;
        }

        public int DeleteVendor(int VendorID)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllVendorNames()
        {
            List<string> vendorNames = new List<string>();
            try
            {
                vendorNames = _vendorAccessor.GetVendorNames();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vendor names retreival failed", ex);
            }
            return vendorNames;
        }

        public List<Vendor> GetAllVendors()
        {
            List<Vendor> vendors = new List<Vendor>();
            try
            {
                vendors = _vendorAccessor.GetAllVendors();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Item retreival failed", ex);
            }
            return vendors;
        }

        public Vendor GetItemVendor(int ItemID)
        {
            throw new NotImplementedException();
        }

        public Vendor GetVendorById(int vendorId)
        {
            Vendor vendor = null;
            try
            {
                vendor = _vendorAccessor.GetVendorByID(vendorId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vendor retreival failed", ex);
            }
            return vendor;
        }

        public Vendor GetVendorByName(string name)
        {
            Vendor vendor = null;
            try
            {
                vendor = _vendorAccessor.GetVendorByName(name);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vendor retreival failed", ex);
            }
            return vendor;
        }

        public bool UpdateVendor(int VendorID, Vendor vendor)
        {
            bool result = false;
            result = (1 == _vendorAccessor.UpdateVendor(VendorID, vendor.VendorName, vendor.AccountOwner, vendor.ContactPerson, vendor.PhoneNumber, vendor.Email, vendor.LeadTimeDays));

            return result;
        }
    }
}
