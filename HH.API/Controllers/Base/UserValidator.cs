using HH.API.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Controllers
{
    /// <summary>
    /// 用户权限认证
    /// </summary>
    public partial class APIController
    {
        #region 权限校验 -------------------------
        /// <summary>
        /// 组织权限验证
        /// </summary>
        /// <param name="authorizationMode"></param>
        /// <param name="targetOrgId"></param>
        /// <returns></returns>
        public bool ValidationOrganization(AuthorizationMode authorizationMode, string targetOrgId)
        {
            return true;
        }
        #endregion
    }
}
