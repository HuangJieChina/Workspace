using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Database
{
    public class SqlDbHandler : IDbHandler
    {
        public bool CreateTable(string tableName, Type entityType)
        {
            return true;
        }

        public bool IsExistsTable(string tableName)
        {
            return false;
        }

        public bool ModifyTable(string tableName, Type entityType)
        {
            return true;
        }
    }
}
