using DapperExtensions;
using HH.API.Entity;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    public interface ISsoAuthorizeRepository : IRepositoryBase<SsoSystem>
    {
        /// <summary>
        /// 检验是否有权限访问当前 API 接口
        /// </summary>
        /// <param name="systemCode"></param>
        /// <param name="apiPath"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        bool ValidateAutorize(string systemCode, string apiPath, string methodName);
    }
}