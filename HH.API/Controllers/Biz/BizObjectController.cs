using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.IInterface;
using HH.API.Entity;
using HH.API.Entity.BizObject;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class BizObjectController : APIController
    {
        /// <summary>
        /// 添加业务结构
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public JsonResult AddBizSchema([FromBody] BizSchema schema)
        {
            return Json(new APIResult
            {
            });
        }

        public ActionResult AddUser([FromBody] OrgUser user)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetChildUnitsByParent(string parentId)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetChildUsersByParent(string parentId)
        {
            throw new NotImplementedException();
        }

        public OrgUnit GetUnit(string objectId)
        {
            throw new NotImplementedException();
        }

        public ActionResult RemoveUnit(string objectId)
        {
            throw new NotImplementedException();
        }

        public ActionResult RemoveUser([FromHeader] string objectId)
        {
            throw new NotImplementedException();
        }

        public ActionResult UpdateUnit([FromBody] OrgUnit orgUnit)
        {
            throw new NotImplementedException();
        }

        public ActionResult UpdateUser([FromBody] OrgUser user)
        {
            throw new NotImplementedException();
        }
    }
}
