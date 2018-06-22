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
        private IBizSchemaRepository bizSchemaRepository = null;
        private IBizSheetRepository bizSheetRepository = null;
        private IWorkflowTemplateRepository workflowTemplateRepository = null;
        #endregion

        public WorkflowManagerController(IWorkflowPackageRepository workflowPackageRepository,
            IBizSchemaRepository bizSchemaRepository,
            IBizSheetRepository bizSheetRepository,
            IWorkflowTemplateRepository workflowTemplateRepository)
        {
            this.workflowPackageRepository = workflowPackageRepository;
            this.bizSchemaRepository = bizSchemaRepository;
            this.bizSheetRepository = bizSheetRepository;
            this.workflowTemplateRepository = workflowTemplateRepository;
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
        [HttpGet]
        public JsonResult AddWorkflowPackage([FromHeader]string folderId,
            [FromHeader]string packageCode,
            [FromHeader]string packageName,
            [FromHeader]int sortOrder)
        {
            // 新增 WorkflowPackage
            WorkflowPackage workflowPackage = new WorkflowPackage(folderId, packageCode, packageName, sortOrder);
            this.workflowPackageRepository.Insert(workflowPackage);

            // 新增 数据模型
            BizSchema schema = new BizSchema(packageCode, this.CurrentUser.ObjectId);
            this.bizSchemaRepository.Insert(schema);

            // 新增 默认表单
            BizSheet sheet = new BizSheet();
            sheet.Initial(packageCode, this.CurrentUser.ObjectId);
            this.bizSheetRepository.Insert(sheet);

            // 新增 默认流程
            WorkflowTemplate workflow = new WorkflowTemplate(packageCode,
                packageCode,
                packageName,
                this.CurrentUser.ObjectId,
                sortOrder);
            this.workflowTemplateRepository.Insert(workflow);

            return Json(new APIResult() { Message = "OK", ResultCode = ResultCode.Success });
        }

        /// <summary>
        /// 新增流程模板
        /// </summary>
        /// <param name="workflowTemplate"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AddWorkflowTemplate([FromHeader]string schemaCode,
            [FromHeader]string workflowCode,
            [FromHeader]string displayName,
            [FromHeader]int sortOrder)
        {
            // TODO:校验流程模板编码是否已经存在

            WorkflowTemplate workflow = new WorkflowTemplate(schemaCode,
                workflowCode,
                displayName,
                this.CurrentUser.ObjectId,
                sortOrder);
            this.workflowTemplateRepository.Insert(workflow);

            return Json(new APIResult() { Message = "OK", ResultCode = ResultCode.Success });
        }

        public BizSchema GetBizSchemaByCode([FromHeader]string schemaCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult PublishBizSchema([FromHeader]string schemaCode)
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
