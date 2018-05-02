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

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        // GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpPost("authenticate")]
        public IActionResult Authenticate(dynamic data)//[FromBody]string userCode, [FromBody]string password) // dynamic user)//
        {
            //string userCode = user.userCode;
            //string password = user.password;
            var tokenHandler = new JwtSecurityTokenHandler();
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(JwtClaimTypes.Audience,"api"),
                     new Claim(JwtClaimTypes.Issuer,"YFAPICommomCore"),
                     new Claim(JwtClaimTypes.Id, "1"),
                     new Claim(JwtClaimTypes.Name, "xxx"),
                     new Claim(JwtClaimTypes.Email, "xxx@qq.com"),
                     new Claim(JwtClaimTypes.PhoneNumber, "13500000000")
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(Startup.symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                access_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    sid = "1",
                    name = "xxxx",
                    auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                }
            });
        }

    }

}
