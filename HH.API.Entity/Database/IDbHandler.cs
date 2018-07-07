using HH.API.Entity.BizObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public interface IDbHandler
    {
        /// <summary>
        /// 数据库表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool IsExistsTable(string tableName);

        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <param name="tableName"></param>
        bool CreateTable(string tableName);

        /// <summary>
        /// 修改数据库表
        /// </summary>
        /// <param name="tableName"></param>
        bool ModifyTable(string tableName);

        /// <summary>
        /// 注册业务数据表
        /// </summary>
        /// <param name="bizSchema"></param>
        /// <returns></returns>
        bool RegisterBizTable(BizSchema bizSchema);
    }
}
