using DapperExtensions;
using HH.API.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace HH.API.IServices
{
    /// <summary>
    /// 服务的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IServiceBase
    {
        /// <summary>
        /// 获取服务器的时间，Ticks
        /// </summary>
        /// <returns></returns>
        long GetDateTime();
    }
}