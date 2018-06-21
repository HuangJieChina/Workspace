using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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


        
    }
}
