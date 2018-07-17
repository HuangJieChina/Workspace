using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IServices;
using HH.API.Entity.App;

namespace HH.API.Services
{
    public class FunctionNodeRepository : RepositoryBase<AppCatalog>, IAppCatalogRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FunctionNodeRepository()
        {

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public AppCatalog GetFunctionNodeByName(string parentId, string functionName)
        {
            // 查询条件
            IList<IPredicate> predList = new List<IPredicate>();

            predList.Add(Predicates.Field<AppCatalog>(p => p.ParentId, Operator.Eq, parentId));
            predList.Add(Predicates.Field<AppCatalog>(p => p.FunctionName, Operator.Eq, functionName));

            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            return this.GetSingle(predGroup);
        }

        /// <summary>
        /// 获取子节点集合
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<AppCatalog> GetSubFunctionNodesByParent(string parentId)
        {
            return this.GetListByKey(AppCatalog.PropertyName_ParentId, parentId);
        }
    }
}