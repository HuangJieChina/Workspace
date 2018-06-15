using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.Entity.BizObject;

namespace HH.API.Services
{
    public class BizSchemaRepository : RepositoryBase<BizSchema>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BizSchemaRepository()
        {

        }
    }
}