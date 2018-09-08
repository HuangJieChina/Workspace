using HH.API.Entity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HH.API.IController
{
    /// <summary>
    /// 组织机构服务接口类
    /// </summary>
    public interface ISsoManagerController : ISsoController
    {
        /// <summary>
        /// 新增SsoSystem对象
        /// </summary>
        /// <param name="ssoSystem">SsoSystem对象</param>
        /// <returns>返回新增是否成功</returns>
        [HttpPost]
        JsonResult AddSsoSystem([FromBody]SsoSystem ssoSystem);

        /// <summary>
        /// 更新SsoSystem
        /// </summary>
        /// <param name="ssoSystem">SsoSystem对象</param>
        /// <returns>返回更新是否成功</returns>
        [HttpPost]
        JsonResult UpdateSsoSystem([FromBody]SsoSystem ssoSystem);

        /// <summary>
        /// 删除一个单点登录系统信息
        /// </summary>
        /// <param name="systemCode">系统编码</param>
        /// <returns>返回删除是否成功</returns>
        [HttpGet]
        JsonResult RemoveSsoSystem(string systemCode);
    }
}
