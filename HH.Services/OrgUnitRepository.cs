using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;

namespace HH.API.Services
{
    public class OrgUnitRepository
    {
        public OrgUnitRepository()
        {

        }

        public virtual bool Insert(OrgUnit unit)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                conn.Insert<OrgUnit>(unit);
                return true;
            }
        }
    }
}