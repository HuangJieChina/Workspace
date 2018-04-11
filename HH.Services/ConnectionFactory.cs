﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using DapperExtensions;
using System.Data.Common;
using HH.API.Entity;

namespace HH.API.Services
{
    /// <summary>
    /// 数据库连接工程类
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        private static string ConnectString
        {
            get
            {
                return "Data Source=.;Initial Catalog=HHLog;User id=sa;Password=h3password;";
            }
        }

        /// <summary>
        /// 获取当前数据库类型
        /// </summary>
        public static DatabaseType DatabaseType
        {
            get
            {
                return DatabaseType.SqlServer;
            }
        }

        private static DbConnection CreateConnection<T>()
            where T : DbConnection, new()
        {
            Database d;

            DbConnection connection = new T();
            connection.ConnectionString = ConnectString;
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <returns></returns>
        public static DbConnection DefaultConnection()
        {
            return CreateConnection<SqlConnection>();
        }

    }
}
