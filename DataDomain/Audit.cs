using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Audit
    {
        public int AuditID { get; set; }
        public int UserID { get; set; }
        public string UserFullName { get; set; }
        public string TableName { get; set; }
        public string Operation {  get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string EventType { get; set; }
        public string EventData { get; set; }
        public bool Active { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
