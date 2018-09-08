using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Aop
{
    public static class WhiteListExtensions
    {
        /// <summary>
        /// 白名单中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWhiteList(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WhiteListMiddleware>();
        }
    }
}
