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
        public WhiteListMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IBizSchemaRepository bizSchemaRepository)
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
