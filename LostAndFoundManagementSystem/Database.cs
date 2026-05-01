using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFoundManagementSystem
{
    internal class Database
    {
        public static string connectionString =
          @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LostFoundDB;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
