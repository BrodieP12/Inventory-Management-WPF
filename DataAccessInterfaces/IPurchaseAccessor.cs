using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IPurchaseAccessor
    {
        int AddPurchaseOrder(int UserID, int VendorID, DateTime OrderDate, DateTime ExpectedDeliveryDate, string Status, double TotalAmount);

        int UpdatePurchaseOrder(int UserID, int PurchaseOrderID, int VendorID, DateTime OrderDate, DateTime ExpectedDeliveryDate, string Status, double TotalAmount);
    }
}
