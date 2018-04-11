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
                Name = "Test"
            };
            OrgUnitRepository d = new OrgUnitRepository();
            dynamic result = d.Insert(u);

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
