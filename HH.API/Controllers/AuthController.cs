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

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : APIController
    {
        // GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpPost("authenticate")]
        public APIResult LoginIn([FromBody]dynamic user)
        {
            string userCode = user.userCode;
            string password = user.password;

            ClaimsPrincipal principal = new ClaimsPrincipal();
            principal.Claims.Append<Claim>(new Claim(JwtClaimTypes.Id, Guid.NewGuid().ToString()));
            principal.Claims.Append<Claim>(new Claim(JwtClaimTypes.Name, "huangj"));

            AuthenticationHttpContextExtensions.SignInAsync(HttpContext, "AuthCookie", principal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20), // 20 分钟后过期
                IsPersistent = false,
                AllowRefresh = false
            });

            return new APIResult()
            {
                ResultCode = ResultCode.Success,
                Extend = new
                {
                    sid = "1",
                    name = "xxxx",
                    auth_time = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds(),  // 认证时间
                    expires_at = new DateTimeOffset(DateTime.UtcNow.AddMinutes(20)).ToUnixTimeSeconds() // 过期时间
                }
            };
        }

        [HttpPost("authenticate")]
        public APIResult LoginOut([FromBody]dynamic user)
        {
            AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, "AuthCookie");
            return new APIResult()
            {
                ResultCode = ResultCode.Success
            };
        }

    }

}
