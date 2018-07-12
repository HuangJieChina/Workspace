using DapperExtensions;
using HH.API.Entity;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFunctionNodeRepository : IRepositoryBase<FunctionNode>
    {
        /// <summary>
        /// 根据业务类型获取根节点集合
        /// </summary>
        /// <param name="functionType">业务类型</param>
        /// <returns>根节点集合</returns>
        List<FunctionNode> GetRootFunctionNodesByType(FunctionType functionType);

        /// <summary>
        /// 根据上级节点获取直属子节点
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <returns>子节点集合</returns>
        List<FunctionNode> GetSubFunctionNodesByParent(string parentId);

        /// <summary>
        /// 根据名称获取节点
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="parentId"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        FunctionNode GetFunctionNodeByName(FunctionType functionType, string parentId, string functionName);
    }
}
