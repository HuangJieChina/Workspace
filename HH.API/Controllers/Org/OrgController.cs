using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.IInterface;
using HH.API.Entity;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class OrgController : APIController, IOrgController
    {
        /// <summary>
        /// 添加组织机构对象
        /// </summary>
        /// <param name="orgUnit"></param>
        /// <returns></returns>
        public ActionResult AddUnit([FromBody] OrgUnit orgUnit)
        {
            return new APIResult() { };
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
