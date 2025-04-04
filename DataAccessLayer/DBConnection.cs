using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class DBConnection
    {
        internal static SqlConnection GetConnection()
        {
            SqlConnection conn = null;


            // Data Source: URL to connect to
            // Initial catalog: Database name to connect to
            // Integrated Security: How security will be handled
            // Trust Server certificate: To trust that the database will be self signed
            // 
            string connectionString = @"Data Source=localhost;Initial Catalog=IMS_db;Integrated Security=True;Trust Server Certificate=True";

            conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
