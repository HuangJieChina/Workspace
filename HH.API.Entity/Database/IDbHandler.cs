﻿using System;
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
        /// <param name="entityType"></param>
        bool CreateTable(string tableName, Type entityType);

        /// <summary>
        /// 修改数据库表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="entityType"></param>
        bool ModifyTable(string tableName, Type entityType);
    }
}
