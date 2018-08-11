using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Authorization
{
    /// <summary>
    /// 认证方式
    /// </summary>
    public enum AuthorizationType
    {
        /// <summary>
        /// 用户认证
        /// </summary>
        User = 0,
        /// <summary>
        /// 系统认证
        /// </summary>
        System = 1
    }
}