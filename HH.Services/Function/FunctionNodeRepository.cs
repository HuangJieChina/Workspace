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
    public class FunctionNodeRepository : RepositoryBase<FunctionNode>, IFunctionNodeRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FunctionNodeRepository()
        {

        }

        public FunctionNode GetFunctionNodeByName(FunctionType functionType, string parentId, string functionName)
        {
            // 查询条件
            IList<IPredicate> predList = new List<IPredicate>();

            predList.Add(Predicates.Field<FunctionNode>(p => p.FunctionType, Operator.Eq, (int)functionType));
            if (string.IsNullOrEmpty(parentId))
            {
                predList.Add(Predicates.Field<FunctionNode>(p => p.IsRoot, Operator.Eq, "1"));
            }
            else
            {
                predList.Add(Predicates.Field<FunctionNode>(p => p.ParentId, Operator.Eq, parentId));
            }
            predList.Add(Predicates.Field<FunctionNode>(p => p.FunctionName, Operator.Eq, functionName));

            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            return this.GetSingle(predGroup);
        }

        /// <summary>
        /// 根据业务类型获取根节点集合
        /// </summary>
        /// <param name="functionType">业务类型</param>
        /// <returns></returns>
        public List<FunctionNode> GetRootFunctionNodesByType(FunctionType functionType)
        {
            // 查询条件
            IList<IPredicate> predList = new List<IPredicate>();
            // 类型
            predList.Add(Predicates.Field<FunctionNode>(p => p.FunctionType, Operator.Eq, (int)functionType));
            // 根节点
            predList.Add(Predicates.Field<FunctionNode>(p => p.IsRoot, Operator.Eq, true));
            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            // 排序字段
            List<ISort> sort = new List<ISort>();
            sort.Add(new Sort() { Ascending = true, PropertyName = FunctionNode.PropertyName_SortOrder });

            return this.GetList(predGroup, sort);
        }

        /// <summary>
        /// 获取子节点集合
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<FunctionNode> GetSubFunctionNodesByParent(string parentId)
        {
            return this.GetListByKey(FunctionNode.PropertyName_ParentId, parentId);
        }
    }
}