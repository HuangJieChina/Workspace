using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.Entity.Orgainzation;
using HH.API.Entity.Cache.KeyCollectionCache;
using System.Data.Common;
using HH.API.IRepository;

namespace HH.API.Repository
{
    public class OrgDepartmentRepository : RepositoryBase<OrgDepartment>, IOrgDepartmentRepository
    {
        public OrgDepartmentRepository() : base()
        {
        }

        private IKeyCollectionCache<String> _ParentUnitIds = null;

        protected IKeyCollectionCache<String> ParentUnitIds
        {
            get
            {
                if (_ParentUnitIds == null)
                {
                    _ParentUnitIds = KeyCollectionCacheFactory<String>.Instance.GetCache(OrgDepartment.PropertyName_ParentId);
                }
                return _ParentUnitIds;
            }
        }


        /// <summary>
        /// 获取组织根节点
        /// </summary>
        public OrgDepartment RootDepartment
        {
            get
            {
                return this.GetObjectByKey(OrgDepartment.PropertyName_IsRootUnit, "1");
            }
        }

        /// <summary>
        /// 判断一个组织是否另外一个组织的上级
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        public bool IsParentDepartment(string parentId, string childId)
        {
            List<string> parents = this.GetParentDepartmentIds(childId);
            return parents.Contains(parentId);
        }

        /// <summary>
        /// 递归获取一个组织的所有父组织Id集合
        /// </summary>
        /// <param name="orgUnitId"></param>
        /// <returns></returns>
        public List<string> GetParentDepartmentIds(string orgUnitId)
        {
            if (!ParentUnitIds.ContainsKey(orgUnitId))
            {
                List<string> parents = new List<string>();

                OrgDepartment orgUnit = this.GetObjectById(orgUnitId);
                // 防止死循环
                int index = 0;
                while (!orgUnit.IsRootUnit)
                {
                    if (index > 100) throw new APIException(APIResultCode.StackOverflow,
                        string.Format("获取组织对象Id='{0}'的上级", orgUnitId));

                    parents.Insert(0, orgUnit.ObjectId);
                    if (orgUnit != null)
                    {
                        orgUnit = this.GetObjectById(orgUnit.ParentId);
                    }
                    index++;
                }
                this.ParentUnitIds.Save(orgUnitId, parents);
            }
            return this.ParentUnitIds.Get(orgUnitId);
        }

        /// <summary>
        /// 判断一个组织范围是否包含一个组织
        /// </summary>
        /// <param name="unitScopes"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        public bool UnitScopesContains(List<string> unitScopes, string childId)
        {
            if (unitScopes == null || unitScopes.Count == 0) return false;
            List<string> parentUnitIds = this.GetParentDepartmentIds(childId);
            return unitScopes.Find((x) => { return parentUnitIds.Contains(x); }) != null;
        }

        /// <summary>
        /// 是否存在指定名称的组织对象
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public bool IsExistsOrgName(string parentId, string displayName)
        {
            IList<IPredicate> predList = new List<IPredicate>();
            predList.Add(Predicates.Field<OrgDepartment>(p => p.ParentId, Operator.Eq, parentId));
            predList.Add(Predicates.Field<OrgDepartment>(p => p.DisplayName, Operator.Eq, displayName));
            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            return this.GetSingle(predGroup) != null;
        }

        /// <summary>
        /// 查询组织单元
        /// </summary>
        /// <param name="pageIndex">当前页码(从1开始)</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="recordCount">页面记录数</param>
        /// <param name="displayName">组织名称</param>
        /// <returns></returns>
        public List<OrgDepartment> QueryOrgDepartment(int pageIndex, int pageSize, out long recordCount, string displayName)
        {
            // Demo:单表查询分页
            // 查询条件
            IList<IPredicate> predList = new List<IPredicate>();
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                predList.Add(Predicates.Field<OrgDepartment>(p => p.DisplayName, Operator.Like, displayName));
            }
            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            // 执行结果，注:单表查询默认按照CreatedTime倒序排序
            return this.GetPageList(pageIndex, pageSize, out recordCount, predGroup);
        }

        public List<OrgDepartment> GetChildDepartmentsByParent(string parentId, bool recursive)
        {
            throw new NotImplementedException();
        }

        public List<dynamic> QueryDepartmentByManagerId(string managerId)
        {
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2}=@ManagerId",
              OrgUnit.PropertyName_ObjectId, EntityConfig.Table.OrgDepartment, OrgUnit.PropertyName_ManagerId);

            using (DbConnection conn = this.OpenConnection())
            {
                var x = conn.Query(sql, new { ManagerId = managerId }).ToList();
                return x;
            }
        }
    }
}