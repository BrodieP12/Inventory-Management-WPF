using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface ISettingAcccessor
    {
        List<Setting> GetAllUserSettings(int UserId);
        bool UpdateUserSetting(int UserId, string Key, string OldValue, string NewValue);

    }
}
