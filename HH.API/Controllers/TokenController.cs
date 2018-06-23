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

        // GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]dynamic authorization)
        {
            string userCode = authorization.userCode;
            string password = authorization.password;

            var tokenHandler = new JwtSecurityTokenHandler();
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddHours(2); // 设置2个小时内有效

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(JwtClaimTypes.Audience,Config.API_Audience),
                     new Claim(JwtClaimTypes.Issuer,Config.API_Issuer),               // 接口
                     new Claim(JwtClaimTypes.Id, Guid.NewGuid().ToString()),  // 用户的ID
                     new Claim(JwtClaimTypes.Name, "HuangJie"),               // 账号
                     new Claim(JwtClaimTypes.Email, "huangj@authine.com"),    // 邮箱
                     new Claim(JwtClaimTypes.PhoneNumber, "13800138000")      // 手机号码
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(Config.SymmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                AccessToken = tokenString,
                TokenType = TokenType,
                profile = new
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserCode = "huangj",
                    UserName = "黄杰",
                    AuthineTime = new DateTimeOffset(authTime).ToUnixTimeSeconds(),  // 认证时间
                    ExpiresTime = new DateTimeOffset(expiresAt).ToUnixTimeSeconds() // 过期时间
                }
            });
        }

        /// <summary>
        /// 用户和密码认证
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private IActionResult UserAuthenticate(string userCode, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddHours(2); // 设置2个小时内有效

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(JwtClaimTypes.Audience,Config.API_Audience),
                     new Claim(JwtClaimTypes.Issuer,Config.API_Issuer),               // 接口
                     new Claim(JwtClaimTypes.Id, Guid.NewGuid().ToString()),  // 用户的ID
                     new Claim(JwtClaimTypes.Name, "HuangJie"),               // 账号
                     new Claim(JwtClaimTypes.Email, "huangj@authine.com"),    // 邮箱
                     new Claim(JwtClaimTypes.PhoneNumber, "13800138000")      // 手机号码
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(Config.SymmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                AccessToken = tokenString,
                TokenType = TokenType,
                AuthorizationType = AuthorizationType.User,
                profile = new
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserCode = "huangj",
                    UserName = "黄杰",
                    AuthineTime = new DateTimeOffset(authTime).ToUnixTimeSeconds(),  // 认证时间
                    ExpiresTime = new DateTimeOffset(expiresAt).ToUnixTimeSeconds() // 过期时间
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        private IActionResult SystemAuthenticate(string corpId, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddHours(2); // 设置2个小时内有效

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(JwtClaimTypes.Audience,Config.API_Audience),
                     new Claim(JwtClaimTypes.Issuer,Config.API_Issuer),               // 接口
                     new Claim(JwtClaimTypes.Id, Guid.NewGuid().ToString()),  // 用户的ID
                     new Claim(JwtClaimTypes.Name, "HuangJie"),               // 账号
                     new Claim(JwtClaimTypes.Email, "huangj@authine.com"),    // 邮箱
                     new Claim(JwtClaimTypes.PhoneNumber, "13800138000")      // 手机号码
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(Config.SymmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                AccessToken = tokenString,
                TokenType = TokenType,
                AuthorizationType = AuthorizationType.System,
                profile = new
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserCode = "huangj",
                    UserName = "黄杰",
                    AuthineTime = new DateTimeOffset(authTime).ToUnixTimeSeconds(),  // 认证时间
                    ExpiresTime = new DateTimeOffset(expiresAt).ToUnixTimeSeconds() // 过期时间
                }
            });
        }
    }

}
