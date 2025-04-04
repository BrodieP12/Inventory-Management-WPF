using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IAuditAccessor
    {

        List<Audit> GetAllAudits();
        string RevertPreviousAudit(int auditID);
        int PurgeAuditsOlderThan(int userID, DateTime dateTime);
        Audit GetAuditLogById(int auditID);
        int ModifyAuditActive(int userID, bool active);
        int RevertOldAuditRecord(int userID, int auditID);
    }
}
