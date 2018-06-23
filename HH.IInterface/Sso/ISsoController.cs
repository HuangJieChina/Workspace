using HH.API.Entity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HH.API.IController
{
    /// <summary>
    /// 组织机构服务接口类
    /// </summary>
    public interface ISsoController : IBaseController
    {
        /// <summary>
        /// 解析Token获取Token加密前的值
        /// </summary>
        /// <param name="corpId">系统编号</param>
        /// <param name="secret">系统秘钥</param>
        /// <param name="token">加密的Token</param>
        /// <returns>返回Token加密前的值</returns>
        [HttpGet]
        JsonResult GetValueFromToken(string corpId, string secret, string token);

        /// <summary>
        /// 将inputValue加密获取到Token
        /// </summary>
        /// <param name="corpId">系统编号</param>
        /// <param name="secret">系统秘钥</param>
        ///  <param name="targetCorpId">目标系统秘钥</param>
        /// <param name="inputValue">加密的Token</param>
        /// <returns>返回加密后的Token值</returns>
        [HttpGet]
        JsonResult GetToken(string corpId, string secret, string targetCorpId, string inputValue);

        /// <summary>
        /// 获取跳转到其他系统的链接
        /// </summary>
        /// <param name="corpId">当前系统编码</param>
        /// <param name="secret">当前系统秘钥</param>
        /// <param name="targetCorpId">目标系统Id</param>
        /// <param name="userCode">用户账号</param>
        /// <returns>返回单点登录到目标系统的URL地址</returns>
        [HttpGet]
        JsonResult GetSystemDefaultUrl(string corpId, string secret, string targetCorpId, string userCode);

        /// <summary>
        /// 重置系统秘钥
        /// </summary>
        /// <param name="corpId">系统编码</param>
        /// <param name="secret">系统秘钥</param>
        /// <param name="newSecret">新的系统秘钥</param>
        /// <returns>返回更新是否成功</returns>
        [HttpGet]
        JsonResult ResetSecret(string corpId, string secret, string newSecret);
    }
}
