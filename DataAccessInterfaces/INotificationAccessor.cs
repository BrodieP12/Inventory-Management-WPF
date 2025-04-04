using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface INotificationAccessor
    {
        int CreateNotification(int userID, string message, string type);
        int MarkNotificationAsRead(int notificationID);
    }
}
