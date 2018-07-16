using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Reflection;
using DapperExtensions.Sql;
using System.Data.Common;
using HH.API.IServices;
using System.Threading;
using HH.API.Entity.EntityCache;
using HH.API.Entity.KeyCollectionCache;
using HH.API.Entity.Database;

namespace HH.API.Services
{
    /// <summary>
    /// 数据访问基类
    /// TODO:待增加缓存机制
    /// TODO:待增加异步函数
    /// </summary>
    public class DynamicDB
    {
        /// <summary>
        /// 基类构造函数
        /// </summary>
        public DynamicDB()
        {
            // 重新设置默认配置项
            ISqlDialect sqlDialect = null;
            switch (ConnectionFactory.DatabaseType)
            {
                case DatabaseType.MySql:
                    sqlDialect = new MySqlDialect();
                    break;
                case DatabaseType.SqlServer:
                    sqlDialect = new SqlServerDialect();
                    break;
                default:
                    throw new Exception("此数据库类型未实现!");
            }

            DapperExtensions.DapperExtensions.Configure(typeof(ClassMapperBase<>),
                new List<Assembly>(),
                sqlDialect);
        }

        /// <summary>
        /// 获取数据存储表名称
        /// </summary>
        public string TableName
        {
            get
            {
                return "I_Test";
            }
        }

        public void Get()
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                conn.Get<DynimacObject<string>>("xxx");
            }
        }

        /// <summary>
        /// 存储过程的参数
        /// </summary>
        public string ParamterChar
        {
            get
            {
                switch (ConnectionFactory.DatabaseType)
                {
                    case DatabaseType.SqlServer:
                        return "@";
                    case DatabaseType.MySql:
                        return "?";
                    case DatabaseType.Oracle:
                        return ":";
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        // End class
    }
}