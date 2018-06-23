using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IServices;

namespace HH.API.Services
{
    public class OrgRoleRepository : RepositoryBase<OrgRole>, IOrgRoleRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrgRoleRepository()
        {

        }

        public OrgRole GetOrgRoleByCode(string code)
        {
            return this.GetObjectByKey(OrgRole.PropertyName_Code, code);
        }
    }
}