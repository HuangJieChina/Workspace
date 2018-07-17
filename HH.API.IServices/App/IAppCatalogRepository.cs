using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.App;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppCatalogRepository : IRepositoryBase<AppCatalog>
    {
        /// <summary>
        /// 根据上级节点获取直属子节点
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <returns>子节点集合</returns>
        List<AppCatalog> GetSubFunctionNodesByParent(string parentId);

        /// <summary>
        /// 根据名称获取节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        AppCatalog GetFunctionNodeByName(string parentId, string functionName);
    }
}
