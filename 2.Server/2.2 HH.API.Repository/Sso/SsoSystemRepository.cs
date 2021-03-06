﻿using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using HH.API.IRepository;
using HH.API.Common;
using HH.API.Entity.Cache.ObjectCache;

namespace HH.API.Repository
{
    public class SsoSystemRepository : RepositoryBase<SsoSystem>, ISsoSystemRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public SsoSystemRepository() : base()
        {

        }

        /// <summary>
        /// Token缓存Key值
        /// </summary>
        private const string TokenCacheName = "Tokens";
        /// <summary>
        /// Token最大缓存存储量设置
        /// </summary>
        private const int MaxTokenCacheLength = 100000;

        private IListCache<string> _Tokens = null;
        private IListCache<string> Tokens
        {
            get
            {
                if (this._Tokens == null)
                {
                    this._Tokens = ListCacheFactory<string>.Instance.GetCache(
                        string.Format("{0}.{1}", this.GetType().FullName, TokenCacheName),
                        MaxTokenCacheLength);
                }
                return this._Tokens;
            }
        }

        /// <summary>
        /// Token最大存储数量
        /// </summary>
        private const int tokenMaxSize = 10 * 1000;

        /// <summary>
        /// 获取系统
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public SsoSystem GetSsoSystemByCorpId(string corpId)
        {
            SsoSystem ssoSystem = this.GetObjectByKey(SsoSystem.PropertyName_CorpId, corpId);
            // if (ssoSystem == null) throw new Exception(string.Format("CorpId [{0}] is not exists", corpId));
            return ssoSystem;
        }

        /// <summary>
        /// 获取系统
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public SsoSystem GetSsoSystemByCorpId(string corpId, string secret)
        {
            SsoSystem ssoSystem = this.GetSsoSystemByCorpId(corpId);
            // if (!ssoSystem.ValidateSecret(secret)) throw new Exception(string.Format("corpId or secret is not correct.", corpId));
            // if (ssoSystem.IsEnabled) throw new Exception("This system is disenabled!");
            return ssoSystem;
        }

        /// <summary>
        /// 获取登录目标系统的秘钥
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="secret"></param>
        /// <param name="targetCorpId"></param>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public string GetToken(string corpId, string secret, string targetCorpId, string inputValue)
        {
            SsoSystem ssoSystem = this.GetSsoSystemByCorpId(corpId, secret);
            // if (!ssoSystem.AllowGetToken) throw new Exception("This system is not allow get token.");
            return this.GenerateToken(corpId, targetCorpId, inputValue);
        }

        public string GetValueFromToken(string corpId, string secret, string token)
        {
            if (this.Tokens.Contains(token))
            {// 已经有使用，不能再次使用
                throw new Exception("The same Token can't be used 2 times.");
            }
            SsoToken ssoToken = this.GetToken(token);
            if (!ssoToken.TargetCorpId.Equals(corpId))
            {// 检测秘钥是否与目标系统匹配
                throw new Exception(string.Format("This token can't be uesed to current system,which corpId is {0}.",
                    ssoToken.TargetCorpId));
            }
            if (DateTime.Now.Subtract(ssoToken.CreatedTime).TotalSeconds > ssoToken.ExpireSecond)
            {// 检测秘钥是否过期
                throw new Exception("This token is expired.");
            }
            this.Tokens.Add(token);
            this.CleanTokens();

            SsoSystem ssoSystem = this.GetSsoSystemByCorpId(corpId, secret);
            return ssoToken.TokenValue;
        }

        public bool ResetSecret(string corpId, string secret, string newSecret)
        {
            SsoSystem ssoSystem = this.GetSsoSystemByCorpId(corpId, secret);
            ssoSystem.SetSecret(newSecret);
            return true;
        }

        /// <summary>
        /// 生成一个Token
        /// </summary>
        /// <param name="sourceCorpId"></param>
        /// <param name="targetCorpId"></param>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        private string GenerateToken(string sourceCorpId, string targetCorpId, string inputValue)
        {
            SsoToken token = new SsoToken()
            {
                CreatedTime = DateTime.Now,
                ExpireSecond = 30,
                SourceCorpId = sourceCorpId,
                TargetCorpId = targetCorpId,
                TokenValue = inputValue
            };
            string tokenValue = Newtonsoft.Json.JsonConvert.SerializeObject(token);
            return RsaEncryptor.RSAEncrypt(tokenValue);
        }

        /// <summary>
        /// 清理内存
        /// </summary>
        private void CleanTokens()
        {// 非绝对安全
            if (this.Tokens.Count > tokenMaxSize)
            {
                this.Tokens.RemoveAt(tokenMaxSize / 2);
            }
        }

        /// <summary>
        /// 解密获取 Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private SsoToken GetToken(string token)
        {
            string tokenValue = RsaEncryptor.RSADecrypt(token);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SsoToken>(tokenValue);
        }
    }
}