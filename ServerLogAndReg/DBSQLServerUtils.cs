using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Server
{
    class DBSQLServerUtils
    {
        public static SqlConnection conn = new SqlConnection(Server.Properties.Settings.Default.strConnection);
        public static DataTable sqlQuery(string query)
        {
            DataTable data = new DataTable();

            conn.Open();

            SqlCommand command = new SqlCommand(query, conn);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);

            conn.Close();

            return data;
        }

    }
}
