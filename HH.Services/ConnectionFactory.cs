using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using DapperExtensions;
using System.Data.Common;

namespace HH.API.Services
{
    public class ConnectionFactory
    {
        public static DbConnection CreateConnection<T>()
            where T : DbConnection, new()
        {
            DbConnection connection = new T();
            connection.ConnectionString = "Data Source=.;Initial Catalog=HHLog;User id=sa;Password=h3password;";
            connection.Open();
            return connection;
        }

        public static DbConnection CreateSqlConnection()
        {
            return CreateConnection<SqlConnection>();
        }

    }
}
