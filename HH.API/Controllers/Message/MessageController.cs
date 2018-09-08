using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using NLog;
using HH.API.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using HH.API.Entity.Services;
using HH.API.Services.Admin;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : APIController
    {
        /// <summary>
        /// Token认证模式
        /// </summary>
        private const string TokenType = "Bearer";

        /// <summary>
        /// 服务推送至客户端
        /// </summary>
        /// <param name="confgis"></param>
        /// <returns></returns>
        [HttpPost("PushServices")]
        public JsonResult PushServices([FromBody]List<ServiceConfig> confgis)
        {
            ServiceRegister.Instance.Services = confgis;
            return null;
        }

        // End
    }
}