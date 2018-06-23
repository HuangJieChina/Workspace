using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using HH.API.IController;
using HH.API.IServices;
using Microsoft.AspNetCore.Authorization;

namespace HH.API.Controllers
{
    /// <summary>
    /// SSO服务类(允许公开访问的接口)
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class SsoController : APIController, ISsoController
    {
        #region 依赖注入的服务对象 ---------------
        public ISsoSystemRepository ssoSystemRepository = null;
        #endregion

        public SsoController(ISsoSystemRepository ssoSystemRepository)
        {
            this.ssoSystemRepository = ssoSystemRepository;
        }

        public JsonResult GetSystemDefaultUrl(string corpId, string secret, string targetCorpId, string userCode)
        {
            SsoSystem ssoSystem = this.ssoSystemRepository.GetSsoSystemByCorpId(corpId, secret);
            // 目标系统
            SsoSystem targetSystem = this.ssoSystemRepository.GetSsoSystemByCorpId(targetCorpId);

            string defaultUrl = targetSystem.DefaultUrl + string.Empty;
            string token = this.ssoSystemRepository.GetToken(corpId, secret, targetCorpId, userCode);
            defaultUrl += defaultUrl.Contains("?") ? "&" : "?";
            defaultUrl += "Token" + token;
            return Json(defaultUrl);
        }

        public JsonResult GetToken(string corpId, string secret, string targetCorpId, string inputValue)
        {
            string token = this.ssoSystemRepository.GetToken(corpId, secret, targetCorpId, inputValue);

            return Json(token);
        }

        public JsonResult GetValueFromToken(string corpId, string secret, string token)
        {
            string val = this.ssoSystemRepository.GetValueFromToken(corpId, secret, token);
            return Json(token);
        }

        public JsonResult ResetSecret(string corpId, string secret, string newSecret)
        {
            bool res = this.ssoSystemRepository.ResetSecret(corpId, secret, newSecret);
            return Json(res);
        }

    }
}