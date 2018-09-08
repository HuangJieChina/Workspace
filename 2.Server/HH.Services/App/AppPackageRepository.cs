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
        public AppPackageRepository(string corpId) : base(corpId)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public AppPackage GetAppPackageByCode(string appCode)
        {
            return this.GetObjectByKey(AppPackage.PropertyName_ObjectId, appCode);
        }
    }
}