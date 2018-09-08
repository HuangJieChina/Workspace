using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Authorization
{
    /// <summary>
    /// 权限模型
    /// </summary>
    public enum AuthorizationMode
    {
        /// <summary>
        /// 查看
        /// </summary>
        View = 0,
        /// <summary>
        /// 管理
        /// </summary>
        Admin = 1,
        /// <summary>
        /// 拒绝
        /// </summary>
        Refuse = 2
    }
}
