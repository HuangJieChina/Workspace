using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IServices;
using HH.API.Entity.BizModel;

namespace HH.API.Services
{
    public class BizSheetRepository : RepositoryBase<BizSheet>, IBizSheetRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BizSheetRepository()
        {

        }

        public BizSheet GetBizSheetByCode(string sheetCode)
        {
            return this.GetObjectByKey(BizSheet.PropertyName_SheetCode, sheetCode);
        }
    }
}