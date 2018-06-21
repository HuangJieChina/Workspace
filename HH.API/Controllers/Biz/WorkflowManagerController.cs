using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.IInterface;
using HH.API.Entity;
using HH.API.Entity.BizObject;
using HH.API.Services;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class WorkflowManagerController : APIController, IWorkflowManagerController
    {
        public JsonResult AddBizProperty([FromBody] BizProperty property)
        {
            throw new NotImplementedException();
        }

        public JsonResult AddBizSheet([FromBody] BizSheet bizSheet)
        {
            throw new NotImplementedException();
        }

        public JsonResult AddWorkflowFolder([FromBody] FunctionNode functionNode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加流程包
        /// </summary>
        /// <param name="workflowPackage"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddWorkflowPackage([FromBody] WorkflowPackage workflowPackage)
        {
            return Json(new APIResult() { Message = "OK", ResultCode = ResultCode.Success });
        }

        public JsonResult AddWorkflowTemplate([FromBody] WorkflowTemplate workflowTemplate)
        {
            throw new NotImplementedException();
        }

        public BizSchema GetBizSchemaByCode(string schemaCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult PublishBizSchema([FromHeader] string schemaCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult RemoveBizProperty([FromHeader] string schemaCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult RemoveWorkflowPackage([FromHeader] string schemaCode)
        {
            throw new NotImplementedException();
        }
    }
}
