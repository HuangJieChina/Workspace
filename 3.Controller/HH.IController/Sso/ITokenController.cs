using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.IController.Sso
{
    /// <summary>
    /// Token接口
    /// </summary>
    public interface ITokenController
    {
        /// <summary>
        /// 账号密码认证
        /// </summary>
        /// <param name="userCode">用户账号</param>
        /// <param name="password">用户密码</param>
        /// <returns>返回认证是否成功，认证成功则包含Token值</returns>
        [HttpGet]
        IActionResult Authenticate(string userCode, string password);
    }
}
