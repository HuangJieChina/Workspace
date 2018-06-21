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
using HH.API.Entity.BizObject;
using Newtonsoft.Json;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : APIController
    {
        // GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        private List<string> xx = new List<string>();

        [HttpGet("LoginIn")]
        public JsonResult LoginIn([FromHeader]string inputValue)
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
        public JsonResult Method1([FromHeader]string inputValue)
        {
            // Headers需要增加 Authorization:Bearer token
            // 需要先认证才能调用
            return Json(new { Message = "OK," + inputValue });
        }

        [AllowAnonymous]
        [HttpGet("Method2")]
        public JsonResult Method2([FromHeader]string inputValue)
        {
            // Headers需要增加 Authorization:Bearer token
            // 需要先认证才能调用
            return Json(new { Message = "OK" });
        }

        [HttpGet("QueryUser")]
        public JsonResult QueryUser([FromHeader]string inputValue)
        {
            OrgUserRepository userRepository = new OrgUserRepository();
            long userCount = 0;

            List<dynamic> users1 = userRepository.QueryOrgUser1(1, 10, out userCount, "zhangs");

            LogManager.GetCurrentClassLogger().Debug("错误信息{0}");

            return Json(new { Users = users1, Count = userCount });
        }

        [HttpGet("QueryUser1")]
        public APIResult QueryUser1([FromHeader]string inputValue)
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

        [HttpGet("Test1/{inputValue}")]
        public string Test1(string inputValue)
        {
            return inputValue;
        }

        [HttpGet("Test2")]
        public string Test2([FromHeader]string inputValue)
        {
            return inputValue;
        }

        [AllowAnonymous]
        [HttpPost("Test3")]
        public string Test3([FromBody]dynamic obj)
        {
            return "Test3." + obj.inputValue;
        }

        [Authorize]
        [HttpPost("TestAuth")]
        [HttpGet("TestAuth")]
        public string TestAuth(string inputValue)
        {
            return "Test auth->" + inputValue + "," + CurrentUser.ObjectId;
        }

        // GET api/values/5
        [HttpGet]
        public APIResult AddUnit(OrgUnit unit)
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

            OrgUnit u = new OrgUnit()
            {
                ObjectId = Guid.NewGuid().ToString(),
                DisplayName = "Test"
            };
            OrgUnitRepository d = new OrgUnitRepository();
            dynamic result = d.Insert(u);

            List<OrgUnit> units = d.GetAll();

            units[0].DisplayName = "修改后的名称" + DateTime.Now.ToLongTimeString();
            d.Update(units[0]);


            long recordCount = 0;
            List<OrgUnit> list = d.QueryOrgUnit(1, 10, out recordCount, string.Empty);

            d.RemoveObjectById("350f2620-171e-444f-8d19-d01e1853e2e0");


            OrgUserRepository userRepository = new OrgUserRepository();
            long userCount = 0;
            List<dynamic> users = userRepository.QueryOrgUser(1, 10, out userCount, string.Empty);

            List<dynamic> users1 = userRepository.QueryOrgUser1(1, 10, out userCount, "zhangs");

            LogManager.GetCurrentClassLogger().Debug("错误信息{0}");
            return new APIResult() { };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]OrgUnit unit)
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
