using System;
using System.Collections.Generic;
using System.Text;
using HH.API.Entity.BizModel;

namespace HH.API.Entity.Database
{
    public class OracleDbHandler<T> : IDbHandler where T : EntityBase
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

        public bool RegisterBizTable(BizSchema bizSchema)
        {
            throw new NotImplementedException();
        }
    }
}
