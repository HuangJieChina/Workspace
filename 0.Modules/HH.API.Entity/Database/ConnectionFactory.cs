﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using DapperExtensions;
using System.Data.Common;
using HH.API.Entity;

namespace HH.API.Entity.Database
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

        /// <summary>
        /// 创建数据库连接对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static DbConnection CreateConnection<T>()
            where T : DbConnection, new()
        {
            // TODO: 如果SQL连接池超过最大数了，会造成异常
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
            switch (DatabaseType)
            {
                case DatabaseType.SqlServer:
                    return CreateConnection<SqlConnection>();
                default:
                    throw new Exception("数据库连接方式未实现->" + DatabaseType.ToString());
            }
        }

        #region SQL 操作 ------------------------------
        /// <summary>
        /// 执行 SQL 语句返回单个数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            using (DbConnection conn = ConnectionFactory.DefaultConnection())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 执行SQL返回影响结果行
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            using (DbConnection conn = ConnectionFactory.DefaultConnection())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                return cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 执行SQL语句返回 DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static void ExecuteSqlReader(string sql, Action<DbDataReader> action)
        {
            using (DbConnection conn = ConnectionFactory.DefaultConnection())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                action(reader);
            }
        }


        #endregion

    }
}
