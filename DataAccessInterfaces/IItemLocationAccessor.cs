using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IItemLocationAccessor
    {
        ItemLocation GetItemLocation(int ItemID);
        List<ItemLocation> GetAllLocations();
        int UpdateItemLocation(int ItemID, string LocationGUID, string LocationName);
        int AdditemLocation(int ItemID, string LocationGUID, string LocationName);
    }
}
