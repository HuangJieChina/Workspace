using DapperExtensions;
using HH.API.Entity;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    public interface IOrgUserRepository : IRepositoryBase<OrgUser>
    {
        /// <summary>
        /// 根据编码获取用户
        /// </summary>
        /// <param name="code">用户编码</param>
        /// <returns>返回用户对象</returns>
        OrgUser GetOrgUserByCode(string code);

        /// <summary>
        /// 根据父组织Id获取用户集合
        /// </summary>
        /// <param name="parentId">父组织Id</param>
        /// <param name="recursive">是否递归到所有下级组织</param>
        /// <returns>返回用户集合</returns>
        List<OrgUser> GetChildUsersByParent(string parentId, bool recursive);
    }
}
