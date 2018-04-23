using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using NLog;
using HH.API.Services;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet]
        public APIResult AddUnit(OrgUnit unit)
        {
            OrgUnit u = new OrgUnit()
            {
                ObjectId = Guid.NewGuid().ToString(),
                Name = "Test"
            };
            OrgUnitRepository d = new OrgUnitRepository();
            dynamic result = d.Insert(u);

            List<OrgUnit> units = d.GetAll();

            units[0].Name = "修改后的名称" + DateTime.Now.ToLongTimeString();
            d.Update(units[0]);

            d.RemoveObjectById("350f2620-171e-444f-8d19-d01e1853e2e0");

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
