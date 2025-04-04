using Azure;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AuditAccessor : IAuditAccessor
    {
        public List<Audit> GetAllAudits()
        {
            List<Audit> audits = new List<Audit>();

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_select_all_audits", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    audits.Add(new Audit()
                    {
                        AuditID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        TableName = reader.GetString(2),
                        Operation = reader.GetString(3),
                        KeyValues = reader.GetString(4),
                        OldValues = reader.GetString(5),
                        NewValues = reader.GetString(6),
                        EventType = reader.GetString(7),
                        EventData = reader.GetString(8),
                        Timestamp = reader.GetDateTime(9), 
                        Active = reader.GetBoolean(10)
                    });
                }
                return AddNamesToAudits(audits);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Audit> AddNamesToAudits(List<Audit> audits)
        {
            UserAccessor userAccessor = new UserAccessor();
            List<User> users = userAccessor.GetAllUsers();
            foreach (Audit audit in audits)
            {
                foreach (User user in users)
                {
                    if(user.UserID == audit.UserID)
                    {
                        audit.UserFullName = user.GivenName + " " + user.FamilyName;
                    }
                }
            }
            return audits;
        }

        public Audit GetAuditLogById(int auditID)
        {
            throw new NotImplementedException();
        }

        public int ModifyAuditActive(int userID, bool active)
        {
            throw new NotImplementedException();
        }

        public int PurgeAuditsOlderThan(int userID, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public int RevertOldAuditRecord(int userID, int auditID)
        {
            throw new NotImplementedException();
        }

        public string RevertPreviousAudit(int auditID)
        {
            throw new NotImplementedException();
        }
    }
}
