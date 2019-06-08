using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.Orgainzation;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    public interface IOrgUnitRepository : IRepositoryBase<OrgUnit>
    {
        /// <summary>
        /// 查询组织
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        List<OrgUnit> QueryOrgUnit(int pageIndex, int pageSize, out long recordCount, string displayName);

        /// <summary>
        /// 判断一个父级是否已经存在对应的组织
        /// </summary>
        /// <param name="parentId">上级组织Id</param>
        /// <param name="unitName">组织名称</param>
        /// <returns>指定的组织名称是否存在</returns>
        bool IsExistsOrgName(string parentId, string unitName);

        /// <summary>
        /// 根据上级组织获取子组织
        /// </summary>
        /// <param name="parentId">上级组织</param>
        /// <param name="recursive">是否递归</param>
        /// <returns>返回子组织对象集合</returns>
        List<OrgUnit> GetChildUnitsByParent(string parentId, bool recursive);

        /// <summary>
        /// 判断一个组织是否另外一个组织的上级
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        bool IsParentUnit(string parentId, string childId);

        /// <summary>
        /// 获取组织根节点
        /// </summary>
        OrgUnit RootUnit { get; }
    }
}
