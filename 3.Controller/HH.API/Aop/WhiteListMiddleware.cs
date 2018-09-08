using HH.API.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.API.Aop
{
    /// <summary>
    /// 白名单中间件
    /// </summary>
    public class WhiteListMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public WhiteListMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            // _logger = loggerFactory.CreateLogger<WhiteListMiddleware>();
        }

        /// <summary>
        /// 白名单机制
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            // TODO:发送开始请求调用的消息，用于记录每次的请求调用
            // TODO:如果是API访问，则验证是否有接口权限
            // TODO:如果是登录用户，那么默认是以H3的API接口访问，则给所有接口权限，只验证用户权限

            // TODO:校验CorpId是否和秘钥匹配

            string remoteIp = context.Connection.RemoteIpAddress.ToString();
            if (remoteIp.EndsWith("100"))
            {// 白名单检查
                LogManager.GetCurrentClassLogger().Error("非法访问->" + remoteIp);
                // throw new Exception("非法请求！");
                await context.Response.WriteAsync("The request is forbidden. Please contact the administrator!",
                    Encoding.UTF8);
            }
            await _next.Invoke(context);
        }
    }
}
