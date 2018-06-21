using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using HH.API.Entity.BizObject;
using HH.API.Services;
using HH.API.IController;
using HH.API.IServices;
using Microsoft.AspNetCore.Authorization;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class WorkflowManagerController : APIController, IWorkflowManagerController
    {
        #region 服务注入对象 --------------------
        /// <summary>
        /// 流程包
        /// </summary>
        private IWorkflowPackageRepository workflowPackageRepository = null;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workflowPackageRepository"></param>
        public WorkflowManagerController(IWorkflowPackageRepository workflowPackageRepository)
        {
            this.workflowPackageRepository = workflowPackageRepository;
        }

        [AllowAnonymous]
        [HttpGet("Test1")]
        public JsonResult Test1(string inputV)
        {
            this.workflowPackageRepository.Count++;
            inputV = this.workflowPackageRepository.SayHello(inputV);
            return Json(new { X = "OK=" + inputV });
        }

        [HttpPost]
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
        public JsonResult AddWorkflowPackage([FromBody]WorkflowPackage workflowPackage)
        {
            // 新增 WorkflowPackage
            this.workflowPackageRepository.Insert(workflowPackage);
            // 新增 数据模型
            // 新增 默认表单
            // 新增 默认流程
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
