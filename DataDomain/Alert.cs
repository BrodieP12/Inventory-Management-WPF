using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Alert
    {
        public int AlertID { get; set; }
        public string AlertType { get; set; }
        public string AlertLevel { get; set; }
        public DateTime TriggeredAt { get; set; }
    }
    public class AlertVM : Alert
    {
        public InventoryItem Item { get; set; }
    }
}
