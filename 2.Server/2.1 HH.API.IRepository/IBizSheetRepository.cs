﻿using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.BizModel;
using System;
using System.Collections.Generic;

namespace HH.API.IRepository
{
    /// <summary>
    /// 业务表单存储
    /// </summary>
    public interface IBizSheetRepository : IRepositoryBase<BizSheet>
    {
        /// <summary>
        /// 获取或设置表单编码
        /// </summary>
        /// <param name="sheetCode"></param>
        /// <returns></returns>
        BizSheet GetBizSheetByCode(string sheetCode);
    }
}
