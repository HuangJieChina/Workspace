﻿using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;

namespace HH.API.Services
{
    public class OrgUnitRepository : RepositoryBase<OrgUnit>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrgUnitRepository()
        {

        }

        /// <summary>
        /// 查询组织单元
        /// </summary>
        /// <param name="pageIndex">当前页码(从1开始)</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="recordCount">页面记录数</param>
        /// <param name="displayName">组织名称</param>
        /// <returns></returns>
        public List<OrgUnit> QueryOrgUnit(int pageIndex, int pageSize, out long recordCount, string displayName)
        {
            // Demo:单表查询分页
            // 查询条件
            IList<IPredicate> predList = new List<IPredicate>();
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                predList.Add(Predicates.Field<OrgUnit>(p => p.DisplayName, Operator.Like, displayName));
            }
            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            // 执行结果，注:单表查询默认按照CreatedTime倒序排序
            return this.GetPageList(pageIndex, pageSize, out recordCount, predGroup);
        }

    }
}