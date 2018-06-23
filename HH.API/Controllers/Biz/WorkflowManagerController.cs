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
        public IFunctionNodeRepository functionNodeRepository = null;
        #endregion

        public WorkflowManagerController(IWorkflowPackageRepository workflowPackageRepository,
            IBizSchemaRepository bizSchemaRepository,
            IBizSheetRepository bizSheetRepository,
            IWorkflowTemplateRepository workflowTemplateRepository,
            IFunctionNodeRepository functionNodeRepository)
        {
            this.workflowPackageRepository = workflowPackageRepository;
            this.bizSchemaRepository = bizSchemaRepository;
            this.bizSheetRepository = bizSheetRepository;
            this.workflowTemplateRepository = workflowTemplateRepository;
            this.functionNodeRepository = functionNodeRepository;
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

        [HttpGet]
        public JsonResult AddWorkflowFolder(string parentId,
            string functionName,
            int sortOrder,
            bool isRoot)
        {
            FunctionNode functionNode = new FunctionNode(parentId, functionName,
                this.Authorization.ObjectId,
                sortOrder,
                isRoot,
                FunctionType.WorkflowPackage);

            this.functionNodeRepository.Insert(functionNode);

            return Json(functionNode);
        }

        [HttpGet]
        public JsonResult GetRootFolders()
        {
            List<FunctionNode> roots = this.functionNodeRepository.GetRootFunctionNodesByType(FunctionType.WorkflowPackage);
            return Json(roots);
        }

        [HttpGet]
        public JsonResult GetSubFolders( string parentId)
        {
            List<FunctionNode> functionNodes = this.functionNodeRepository.GetSubFunctionNodesByParent(parentId);
            return Json(functionNodes);
        }

        /// <summary>
        /// 添加流程包
        /// </summary>
        /// <param name="workflowPackage"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AddWorkflowPackage(string folderId,
            string packageCode,
            string packageName,
            int sortOrder)
        {
            // 新增 WorkflowPackage
            WorkflowPackage workflowPackage = new WorkflowPackage(folderId, packageCode, packageName, sortOrder);
            this.workflowPackageRepository.Insert(workflowPackage);

            // 新增 数据模型
            BizSchema schema = new BizSchema(packageCode, this.Authorization.ObjectId);
            this.bizSchemaRepository.Insert(schema);

            // 新增 默认表单
            BizSheet sheet = new BizSheet();
            sheet.Initial(packageCode, this.Authorization.ObjectId);
            this.bizSheetRepository.Insert(sheet);

            // 新增 默认流程
            WorkflowTemplate workflow = new WorkflowTemplate(packageCode,
                packageCode,
                packageName,
                this.Authorization.ObjectId,
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
        public JsonResult AddWorkflowTemplate(string schemaCode,
            string workflowCode,
            string displayName,
            int sortOrder)
        {
            // TODO:校验流程模板编码是否已经存在
            WorkflowTemplate workflow = new WorkflowTemplate(schemaCode,
                workflowCode,
                displayName,
                this.Authorization.ObjectId,
                sortOrder);
            this.workflowTemplateRepository.Insert(workflow);

            return Json(new APIResult() { Message = "OK", ResultCode = ResultCode.Success });
        }

        public JsonResult GetWorkflowTemplate(string workflowCode, int workflowVersion)
        {
            throw new NotImplementedException();
        }

        public JsonResult GetDefaultWorkflowTemplate(string workflowCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult GetDesignerWorkflowTemplate(string workflowCode)
        {
            throw new NotImplementedException();
        }

        public BizSchema GetBizSchemaByCode(string schemaCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult PublishBizSchema(string schemaCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult RemoveBizProperty( string schemaCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult RemoveWorkflowPackage( string schemaCode)
        {
            throw new NotImplementedException();
        }

    }
}
