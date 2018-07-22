using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IServices;
using HH.API.Entity.App;

namespace HH.API.Services
{
    public class AppPackageRepository : RepositoryBase<AppPackage>, IAppPackageRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AppPackageRepository()
        {

        }
        
    }
}