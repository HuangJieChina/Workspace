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

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : APIController
    {
        /// <summary>
        /// Token认证模式
        /// </summary>
        private const string TokenType = "Bearer";

        /// <summary>
        /// 服务器认证
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]dynamic authorization)
        {
            string corpId = authorization.corpId;
            string secret = authorization.secret;

            var tokenHandler = new JwtSecurityTokenHandler();
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddHours(2); // 设置2个小时内有效

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(JwtClaimTypes.Audience,Config.API_Audience),
                     new Claim(JwtClaimTypes.Issuer,Config.API_Issuer),     // 接口
                     new Claim(JwtClaimTypes.Id, corpId)                    // 用户的ID
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(Config.SymmetricKey,
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                AccessToken = tokenString,
                TokenType = TokenType,
                // AuthType = this.AuthorizationType,
                profile = new
                {
                    AuthineTime = new DateTimeOffset(authTime).ToUnixTimeSeconds(),  // 认证时间
                    ExpiresTime = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()  // 过期时间
                }
            });
        }

        // End
    }
}