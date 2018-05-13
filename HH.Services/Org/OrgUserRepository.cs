using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;

namespace HH.API.Services
{
    public class OrgUserRepository : RepositoryBase<OrgUser>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrgUserRepository()
        {

        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="pageIndex">当前页码(从1开始)</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="recordCount">页面记录数</param>
        /// <param name="displayName">组织名称</param>
        /// <returns></returns>
        public List<dynamic> QueryOrgUser(int pageIndex, int pageSize, out long recordCount, string displayName)
        {
            recordCount = 0;
            string txt = string.Format("SELECT a.Code,a.DisplayName,b.DisplayName as OUName FROM {0} a JOIN {1} b ON a.ParentId=b.ObjectID  where a.DisplayName like '%'+@DisplayName+'%'",
               EntityConfig.Table.OrgUser, EntityConfig.Table.OrgUnit);
            using (DbConnection conn = this.OpenConnection())
            {
                var x = conn.Query(txt, new { DisplayName = "zhang" }).ToList();
                return x;
            }
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="pageIndex">当前页码(从1开始)</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="recordCount">页面记录数</param>
        /// <param name="displayName">组织名称</param>
        /// <returns></returns>
        public List<dynamic> QueryOrgUser1(int pageIndex, int pageSize, out long recordCount, string displayName)
        {
            recordCount = 0;

            List<dynamic> res = new List<dynamic>();
            string selectQuery = string.Format("SELECT a.Code,a.DisplayName,b.DisplayName as OUName FROM {0} a JOIN {1} b ON a.ParentId=b.ObjectID",
               EntityConfig.Table.OrgUser, EntityConfig.Table.OrgUnit);

            using (DbConnection conn = this.OpenConnection())
            {
                IList<IPredicate> predList = new List<IPredicate>();
                if (!string.IsNullOrWhiteSpace(displayName))
                {
                    IFieldPredicate fieldPredicate = Predicates.Field<OrgUser>(p => p.DisplayName, Operator.Like, displayName, "a");
                    predList.Add(fieldPredicate);
                }

                IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

                List<ISort> sort = new List<ISort>();
                sort.Add(new Sort() { Ascending = false, PropertyName = "b." + EntityBase.PropertyName_CreatedTime });

                res = conn.GetPage<dynamic>(selectQuery, predGroup, sort, 1, 10).ToList<dynamic>();

            }

            return res;
        }

    }
}