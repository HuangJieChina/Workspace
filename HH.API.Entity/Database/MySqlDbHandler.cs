using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Database
{
    public class MySqlDbHandler<T> : IDbHandler where T : EntityBase
    {
        public bool CreateTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public bool IsExistsTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public bool ModifyTable(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}
