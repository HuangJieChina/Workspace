﻿using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IServices;

namespace HH.API.Services
{
    public class OrgGroupRepository : RepositoryBase<OrgGroup>, IOrgGroupRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrgGroupRepository()
        {

        }
    }
}