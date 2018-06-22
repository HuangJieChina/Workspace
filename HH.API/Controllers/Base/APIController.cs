using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using NLog;
using HH.API.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HH.API.Controllers
{
    /// <summary>
    /// Controller 基类
    /// </summary>
    // [Authorize]
    public class APIController : Controller
    {
        /// <summary>
        /// 获取当前用户(只有非匿名方法才能访问)
        /// </summary>
        public OrgUser CurrentUser
        {
            get
            {
                // 调试用
                return new OrgUser() { };

                #region 正式用 -------------
                //ClaimsIdentity identity = User.Identity as ClaimsIdentity;
                //if (identity == null || identity.Claims.Count() == 0) throw new Exception("禁止在匿名方法获取当前用户信息");
                //string id = identity.Claims.FirstOrDefault(u => u.Type == JwtClaimTypes.Id).Value;
                //OrgUser user = new OrgUser()
                //{
                //    ObjectId = id
                //};
                //return user;
                #endregion
            }
        }


    }
}