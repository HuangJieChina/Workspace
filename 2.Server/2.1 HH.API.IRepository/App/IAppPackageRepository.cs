using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.App;
using System;
using System.Collections.Generic;

namespace HH.API.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppPackageRepository : IRepositoryBase<AppPackage>
    {
        /// <summary>
        /// 根据编码获取应用包对象
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        AppPackage GetAppPackageByCode(string appCode);
    }
}
