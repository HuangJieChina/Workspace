using HH.API.Entity.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity
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

        public IDbHandler GetDefaultDbHandler()
        {
            return this.GetDbHandler(ConnectionFactory.DatabaseType);
        }

        /// <summary>
        /// 获取数据库访问
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public IDbHandler GetDbHandler(DatabaseType databaseType)
        {
            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                    return new SqlDbHandler();
                case DatabaseType.Oracle:
                    return new OracleDbHandler();
                case DatabaseType.MySql:
                    return new MySqlDbHandler();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
