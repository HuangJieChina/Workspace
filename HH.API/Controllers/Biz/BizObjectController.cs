using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using HH.API.IServices;
using HH.API.Entity.BizData;
using HH.API.Entity.BizModel;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class BizObjectController : APIController
    {
        public BizObjectController(IBizObjectRepository bizObjectRepository)
        {
            this.bizObjectRepository = bizObjectRepository;
        }

        private IBizObjectRepository bizObjectRepository = null;

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

        [HttpPost("AddBizObject")]
        public JsonResult AddBizObject([FromBody]BizObject bizObject)
        {
            // 当前访问的用户
            bizObject.CreatedBy = string.Empty;

            dynamic res = this.bizObjectRepository.AddBizObject(bizObject);

            return Json(res);
        }

    }
}
