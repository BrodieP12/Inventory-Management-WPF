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
    public class AuditManager : IAuditManager
    {
        private IAuditAccessor _auditAccessor;
        public AuditManager()
        {
            _auditAccessor = new AuditAccessor();
        }
        public AuditManager(IAuditAccessor auditAccessor)
        {
            _auditAccessor = auditAccessor;
        }
        public List<Audit> GetAllAudits()
        {
            List<Audit> audits = new List<Audit>();
            try
            {
                audits = _auditAccessor.GetAllAudits();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Audit retreival failed", ex);
            }

            return audits; 
        }

        public List<Audit> GetAllAuditsByActive(bool active)
        {
            throw new NotImplementedException();
        }

        public bool PurgeAuditsAfterDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public bool RevertRecordFromAudit(int auditId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAuditActive(int auditId, bool active)
        {
            throw new NotImplementedException();
        }
    }
}
