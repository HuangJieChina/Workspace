using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using HH.API.IController;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class OrgController : APIController, IOrgController
    {
        public JsonResult AddUnit([FromBody] OrgUnit orgUnit)
        {
            throw new NotImplementedException();
        }

        public JsonResult AddUser([FromBody] OrgUser user)
        {
            throw new NotImplementedException();
        }

        public JsonResult GetChildUnitsByParent(string parentId)
        {
            throw new NotImplementedException();
        }

        public JsonResult GetChildUsersByParent(string parentId)
        {
            throw new NotImplementedException();
        }

        public OrgUnit GetUnit(string objectId)
        {
            throw new NotImplementedException();
        }

        public JsonResult RemoveUnit( string objectId)
        {
            throw new NotImplementedException();
        }

        public JsonResult RemoveUser( string objectId)
        {
            throw new NotImplementedException();
        }

        public JsonResult UpdateUnit([FromBody] OrgUnit orgUnit)
        {
            throw new NotImplementedException();
        }

        public JsonResult UpdateUser([FromBody] OrgUser user)
        {
            throw new NotImplementedException();
        }
    }
}
