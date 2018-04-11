using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Database
{
    public class MySqlDbHandler : IDbHandler
    {
        public bool CreateTable(string tableName, Type entityType)
        {
            throw new NotImplementedException();
        }

        public bool IsExistsTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public bool ModifyTable(string tableName, Type entityType)
        {
            throw new NotImplementedException();
        }
    }
}
