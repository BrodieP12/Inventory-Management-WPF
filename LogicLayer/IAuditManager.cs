using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IAuditManager
    {
        List<Audit> GetAllAudits();
        List<Audit> GetAllAuditsByActive(bool active);
        bool PurgeAuditsAfterDate(DateTime dateTime);
        bool UpdateAuditActive(int auditId, bool active);
        bool RevertRecordFromAudit(int auditId);
    }
}
