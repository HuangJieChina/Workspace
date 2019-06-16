using DapperExtensions;
using HH.API.Entity.App;
using System;
using System.Collections.Generic;

namespace HH.API.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppFunctionRepository : IRepositoryBase<AppFunction>
    {
        /// <summary>
        /// 根据上级节点获取直属子节点
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <returns>子节点集合</returns>
        List<AppFunction> GetSubFunctionByParent(string parentId);

        /// <summary>
        /// 根据名称获取节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        AppFunction GetFunctionByName(string parentId, string functionName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        AppFunction GetFunctionByCode(string functionCode);
    }
}
