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
using HH.API.Entity.Orgainzation;
using HH.API.Authorization;
using System.Threading;

namespace HH.API.Controllers
{
    /// <summary>
    /// Controller 基类
    /// </summary>
    // [Authorize]
    [EnableCors("AllowAllOrigin")]  // 跨域支持
    public partial class APIController : Controller
    {
        #region 属性信息 -------------------------
        /// <summary>
        /// 获取日志写入对象
        /// </summary>
        public Logger LogWriter
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

        /// <summary>
        /// 获取当前已认证的对象
        /// </summary>
        public EntityBase Authorized
        {
            get
            {
                switch (this.AuthorizationType)
                {
                    case AuthorizationType.User:
                        return this.CurrentUser as EntityBase;
                    case AuthorizationType.System:
                        return this.CurrentSystem as EntityBase;
                    default:
                        throw new Exception("没有认证!");
                }
            }
        }

        /// <summary>
        /// 获取当前登录系统的用户
        /// </summary>
        public OrgUser CurrentUser
        {
            get
            {
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
                return new OrgUser();
            }
        }

        /// <summary>
        /// 获取当前认证的系统
        /// </summary>
        public SsoSystem CurrentSystem
        {
            get
            {
                return new SsoSystem() { };
            }
        }

        /// <summary>
        /// 获取当前系统认证类型
        /// </summary>
        public AuthorizationType AuthorizationType
        {
            get { return AuthorizationType.User; }
        }
        #endregion

        #region 数据校验 -------------------------
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
        #endregion

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
        /// 返回结果
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual JsonResult Json(ResultCode resultCode, string message)
        {
            return Json(new APIResult()
            {
                ResultCode = resultCode,
                Message = message
            });
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

        /// <summary>
        /// 获取权限不足返回值
        /// </summary>
        public virtual JsonResult PermissionDenied
        {
            get
            {
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.PermissionDenied,
                    Message = "Current request is rejected because of lack of authority."
                });
            }
        }
        #endregion

        #region 锁策略 ---------------------------
        protected static Dictionary<string, object> lockObject = new Dictionary<string, object>();

        /// <summary>
        /// 获取当前锁定的对象
        /// </summary>
        /// <param name="key"></param>
        public object GetLockObject(string key)
        {
            if (lockObject.ContainsKey(key)) return lockObject[key];
            try
            {
                Monitor.Enter(lockObject);
                if (lockObject.ContainsKey(key)) return lockObject[key];

                lockObject.Add(key, new object());
                return lockObject[key];
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }

        /// <summary>
        /// 执行锁的请求方法(防止并发造成判断错误)
        /// </summary>
        /// <param name="lockKey"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public JsonResult MonitorFunction(string lockKey, Func<JsonResult> func)
        {
            if (string.IsNullOrWhiteSpace(lockKey)) throw new Exception("Lock key can not be empty value.");

            object lockObject = this.GetLockObject(lockKey);

            try
            {
                Monitor.Enter(lockObject);
                return func();
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }
        #endregion

        /// <summary>
        /// 获取服务访问对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetRepository<T>()
        {
            return ServiceFactory.Instance.GetRepository<T>();
        }
    }


}