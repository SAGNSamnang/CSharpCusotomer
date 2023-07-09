using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace CustomerMaintenance
{
    public static class MMABooksDB
    {
        public static SqlConnection GetConnection()
        {
            // If necessary, change the following connection string, so it works for your system
            string connectionString = @"Data Source = MSI\SQLEXPRESS; Initial Catalog = MMABooks; Integrated Security = True";

            SqlConnection conn = new SqlConnection(connectionString);

            return conn;
        }
    }
}
