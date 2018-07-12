using DapperExtensions;
using HH.API.Entity;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    public interface IOrgRoleRepository : IRepositoryBase<OrgRole>
    {
        /// <summary>
        /// 根据编码获取角色对象
        /// </summary>
        /// <param name="code">角色编码</param>
        /// <returns></returns>
        OrgRole GetOrgRoleByCode(string code);
    }
}
