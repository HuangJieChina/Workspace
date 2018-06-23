using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.BizObject;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    public interface ISsoSystemRepository : IRepositoryBase<SsoSystem>
    {
        /// <summary>
        /// 根据CorpId获取单点登录系统
        /// </summary>
        /// <param name="corpId">CorpId</param>
        /// <returns>返回单点登录的系统对象</returns>
        SsoSystem GetSsoSystemByCorpId(string corpId);

        /// <summary>
        /// 根据CorpId获取单点登录系统
        /// </summary>
        /// <param name="corpId">CorpId</param>
        /// <param name="secret">secret</param>
        /// <returns>返回单点登录的系统对象</returns>
        SsoSystem GetSsoSystemByCorpId(string corpId, string secret);

        /// <summary>
        /// 解析Token获取Token加密前的值
        /// </summary>
        /// <param name="corpId">系统编号</param>
        /// <param name="secret">系统秘钥</param>
        /// <param name="token">加密的Token</param>
        /// <returns>返回Token加密前的值</returns>
        string GetValueFromToken(string corpId, string secret, string token);

        /// <summary>
        /// 将inputValue加密获取到Token
        /// </summary>
        /// <param name="corpId">系统编号</param>
        /// <param name="secret">系统秘钥</param>
        /// <param name="targetCorpId">目标系统</param>
        /// <param name="inputValue">加密的Token</param>
        /// <returns>返回加密后的Token值</returns>
        string GetToken(string corpId, string secret, string targetCorpId, string inputValue);

        /// <summary>
        /// 重置系统秘钥
        /// </summary>
        /// <param name="corpId">系统编码</param>
        /// <param name="secret">系统秘钥</param>
        /// <param name="newSecret">新的系统秘钥</param>
        /// <returns>返回更新是否成功</returns>
        bool ResetSecret(string corpId, string secret, string newSecret);
    }
}