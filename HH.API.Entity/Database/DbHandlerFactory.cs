using HH.API.Entity.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Database
{
    public class DbHandlerFactory
    {

        private DbHandlerFactory()
        {

        }

        private static DbHandlerFactory _instance = null;

        public static DbHandlerFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbHandlerFactory();
                }
                return _instance;
            }
        }

        public IDbHandler GetDefaultDbHandler<T>() where T : EntityBase
        {
            return this.GetDbHandler<T>(ConnectionFactory.DatabaseType);
        }

        /// <summary>
        /// 获取数据库访问
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public IDbHandler GetDbHandler<T>(DatabaseType databaseType) where T : EntityBase
        {
            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                    return new SqlDbHandler<T>();
                case DatabaseType.Oracle:
                    return new OracleDbHandler<T>();
                case DatabaseType.MySql:
                    return new MySqlDbHandler<T>();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
