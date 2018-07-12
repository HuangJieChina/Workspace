using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using HH.API.IServices;
using HH.API.Entity.Orgainzation;

namespace HH.API.Services
{
    public class OrgUserRepository : RepositoryBase<OrgUser>, IOrgUserRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrgUserRepository()
        {

        }

        public List<OrgUser> GetChildUsersByParent(string parentId, bool recursive)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据用户编码获取用户
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public OrgUser GetOrgUserByCode(string code)
        {
            return this.GetObjectByKey(OrgUser.PropertyName_Code, code);
        }

        #region DEMO 测试 ----------------------
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
            string txt = string.Format("SELECT a.*,b.CnName as OUName FROM {0} a JOIN {1} b ON a.ParentId=b.ObjectID  where a.CnName like '%'+@CnName+'%'",
               EntityConfig.Table.OrgUser, EntityConfig.Table.OrgUnit);
            using (DbConnection conn = this.OpenConnection())
            {
                var x = conn.Query(txt, new { CnName = "zhang" }).ToList();
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
            // Demo:多表查询分页

            // SQL语句
            string sqlCount = string.Format("SELECT Count(1) FROM {0} a JOIN {1} b ON a.ParentId=b.ObjectID",
               EntityConfig.Table.OrgUser, EntityConfig.Table.OrgUnit);
            string sqlQuery = string.Format("SELECT a.*,b.CnName as OUName FROM {0} a JOIN {1} b ON a.ParentId=b.ObjectID",
               EntityConfig.Table.OrgUser, EntityConfig.Table.OrgUnit);

            // 查询条件
            IList<IPredicate> predList = new List<IPredicate>();
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                IFieldPredicate fieldPredicate = Predicates.Field<OrgUser>(p => p.CnName, Operator.Like, "%" + displayName + "%", "a");
                predList.Add(fieldPredicate);
            }
            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            // 排序字段
            List<ISort> sort = new List<ISort>();
            sort.Add(new Sort() { Ascending = false, PropertyName = "b." + EntityBase.PropertyName_CreatedTime });

            // 执行结果
            return this.GetPageList(sqlCount, sqlQuery, pageIndex, pageSize, out recordCount, predGroup, sort);
        }
        #endregion
    }
}