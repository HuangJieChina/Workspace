using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using NLog;
using HH.API.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using HH.API.Entity.BizModel;
using HH.API.Entity.Orgainzation;
using HH.API.IServices;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using HH.API.Repository;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : APIController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

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
                     new Claim(JwtClaimTypes.Issuer,Config.API_Issuer),               // 接口
                     new Claim(JwtClaimTypes.Id, Guid.NewGuid().ToString()),          // 用户的ID
                     new Claim(JwtClaimTypes.Name, "HuangJie")                        // 账号
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(Config.SymmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                AccessToken = tokenString,
                TokenType = "Bearer",
                // AuthType = this.AuthorizationType,
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

        private List<string> xx = new List<string>();

        [HttpGet("LoginIn")]
        public JsonResult LoginIn(string inputValue)
        {
            WorkflowTemplate workflowTemplate = new WorkflowTemplate()
            {
                SchemaCode = "xx",
                WorkflowCode = "yyy"
            };
            string str = JsonConvert.SerializeObject(workflowTemplate);

            xx.Add(DateTime.Now.Ticks.ToString());
            /*测试代码*/
            ClaimsPrincipal principal = new ClaimsPrincipal();
            principal.Claims.Append<Claim>(new Claim(JwtClaimTypes.Id, Guid.NewGuid().ToString()));
            principal.Claims.Append<Claim>(new Claim(JwtClaimTypes.Name, "huangj"));

            AuthenticationHttpContextExtensions.SignInAsync(HttpContext, "AuthCookie", principal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20), // 20 分钟后过期
                IsPersistent = false,
                AllowRefresh = false
            });
            /*End */
            // return Json(new { Message = "OK" });

            return Json(new APIResult { });
        }

        [HttpGet("Method1")]
        public JsonResult Method1(string inputValue)
        {
            // Headers需要增加 Authorization:Bearer token
            // 需要先认证才能调用
            return Json(new { Message = "OK," + inputValue });
        }

        [AllowAnonymous]
        [HttpGet("Method2")]
        public JsonResult Method2(string inputValue)
        {
            // Headers需要增加 Authorization:Bearer token
            // 需要先认证才能调用
            return Json(new { Message = "OK," + inputValue });
        }

        [HttpGet("QueryUser")]
        public JsonResult QueryUser(string inputValue)
        {
            OrgUserRepository userRepository = new OrgUserRepository();
            long userCount = 0;

            List<dynamic> users1 = userRepository.QueryOrgUser1(1, 10, out userCount, "zhangs");

            LogManager.GetCurrentClassLogger().Debug("错误信息{0}");

            return Json(new { Users = users1, Count = userCount });
        }

        [HttpGet("QueryUser1")]
        public APIResult QueryUser1(string inputValue)
        {
            OrgUserRepository userRepository = new OrgUserRepository();
            long userCount = 0;

            List<dynamic> users1 = userRepository.QueryOrgUser1(1, 10, out userCount, "zhangs");

            LogManager.GetCurrentClassLogger().Debug("错误信息{0}");

            return new APIResult()
            {
                Extend = new
                {
                    data = users1,
                    total = userCount
                }
            };
        }

        [HttpGet("Test1/{inputValue1}/{inputValue2}")]
        public JsonResult Test1(string inputValue1, string inputValue2)
        {
            return Json(new { Result = "Test1->" + inputValue1 + "," + inputValue2 });
        }

        [HttpGet("Test2")]
        public JsonResult Test2(string inputValue)
        {
            return Json(new { Result = "Test2->" + inputValue });
        }

        [AllowAnonymous]
        [HttpPost("Test3")]
        public JsonResult Test3([FromBody]dynamic obj)
        {
            return Json(new { Result = "Test3->" + obj.inputValue });
        }

        [Authorize]
        [HttpPost("TestAuth")]
        [HttpGet("TestAuth")]
        public string TestAuth(string inputValue)
        {
            return "Test auth->" + inputValue + ",";// + this.Authorized.ObjectId;
        }

        // GET api/values/5
        [HttpGet("AddUnit")]
        public APIResult AddUnit(OrgDepartment unit)
        {
            TestRepository test = new TestRepository();
            TestParentEntity parent = new TestParentEntity()
            {
                ObjectId = Guid.NewGuid().ToString(),
                ExtendField1 = "A",
                ExtendField2 = "B"
            };
            parent.TestChild = new TestChildEntity()
            {
                RoleId = parent.ObjectId,
                ExtendField1 = "111"
            };
            parent.TestUser = new List<TestUserEntity>();
            parent.TestUser.Add(new TestUserEntity()
            {
                RoleId = parent.ObjectId,
                ExtendField1 = "111"
            });
            parent.TestUser.Add(new TestUserEntity()
            {
                RoleId = parent.ObjectId,
                ExtendField1 = "222"
            });
            test.Insert(parent);

            TestParentEntity testEntity = test.GetObjectById("3abd51f3-e4b1-4a54-bc7c-4e029a9b037c");

            OrgDepartment u = new OrgDepartment()
            {
                ObjectId = Guid.NewGuid().ToString(),
                DisplayName = "Test"
            };
            OrgDepartmentRepository d = new OrgDepartmentRepository();
            dynamic result = d.Insert(u);

            List<OrgDepartment> units = d.GetAll();

            units[0].DisplayName = "修改后的名称" + DateTime.Now.ToLongTimeString();
            d.Update(units[0]);


            long recordCount = 0;
            //List<OrgDepartment> list = d.QueryOrgUnit(1, 10, out recordCount, string.Empty);

            //d.RemoveObjectById("350f2620-171e-444f-8d19-d01e1853e2e0");


            OrgUserRepository userRepository = new OrgUserRepository();
            long userCount = 0;
            List<dynamic> users = userRepository.QueryOrgUser(1, 10, out userCount, string.Empty);

            List<dynamic> users1 = userRepository.QueryOrgUser1(1, 10, out userCount, "zhangs");

            LogManager.GetCurrentClassLogger().Debug("错误信息{0}");
            return new APIResult() { };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]OrgDepartment unit)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
