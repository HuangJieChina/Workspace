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
    public class AppFunctionRepository : RepositoryBase<AppFunction>, IAppFunctionRepository
    {
        public AppFunctionRepository(string corpId) : base(corpId)
        {
        }

        public AppFunction GetFunctionByCode(string functionCode)
        {
            return this.GetObjectByKey(AppFunction.PropertyName_FunctionCode, functionCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public AppFunction GetFunctionByName(string parentId, string functionName)
        {
            // 查询条件
            IList<IPredicate> predList = new List<IPredicate>();

            predList.Add(Predicates.Field<AppFunction>(p => p.ParentId, Operator.Eq, parentId));
            predList.Add(Predicates.Field<AppFunction>(p => p.FunctionName, Operator.Eq, functionName));

            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            return this.GetSingle(predGroup);
        }

        /// <summary>
        /// 获取子节点集合
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<AppFunction> GetSubFunctionByParent(string parentId)
        {
            return this.GetListByKey(AppFunction.PropertyName_ParentId, parentId);
        }
    }
}