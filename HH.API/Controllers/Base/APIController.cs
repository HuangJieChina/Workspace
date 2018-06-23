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
using Microsoft.AspNetCore.Cors;

namespace HH.API.Controllers
{
    /// <summary>
    /// Controller 基类
    /// </summary>
    // [Authorize]
    [EnableCors("AllowAllOrigin")]  // 跨域支持
    public class APIController : Controller
    {
        /// <summary>
        /// 获取当前用户(只有非匿名方法才能访问)
        /// </summary>
        public Authorization Authorization
        {
            get
            {
                // 调试用
                return new Authorization() { };

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

        /// <summary>
        /// 实体格式数据校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected bool DataValidator<T>(T t, out JsonResult result) where T : EntityBase
        {
            List<string> errors = new List<string>();
            bool validate = t.Validate(ref errors);
            if (!validate)
            {
                result = Json(new APIResult()
                {
                    ResultCode = ResultCode.DataFromatError,
                    Message = Newtonsoft.Json.JsonConvert.SerializeObject(errors)
                });
            }
            else
            {
                result = Json(null);
            }
            return validate;
        }

        #region 返回JSON结果扩展 -----------------
        /// <summary>
        /// 返回单个字符串
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual JsonResult Json(string result)
        {
            return Json(new { Result = result });
        }

        /// <summary>
        /// 返回true/false
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual JsonResult Json(bool result)
        {
            return Json(result, string.Empty);
        }

        public virtual JsonResult Json(bool result, string message)
        {
            return Json(new APIResult()
            {
                ResultCode = result ? ResultCode.Success : ResultCode.Error,
                Message = message
            });
        }
        #endregion
    }

    /// <summary>
    /// 认证信息：支持系统用户/密码认证，或者单点登录系统认证
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// 获取或设置访问的对象Id
        /// </summary>
        public string ObjectId { get; set; }

        /// <summary>
        /// 获取或设置系统编号
        /// </summary>
        public string CorpId { get; set; }

        /// <summary>
        /// 获取或设置系统秘钥
        /// </summary>
        public string Secret { get; set; }
    }

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