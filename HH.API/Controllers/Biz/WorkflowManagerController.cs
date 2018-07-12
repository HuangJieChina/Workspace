using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using HH.API.Services;
using HH.API.IController;
using HH.API.IServices;
using Microsoft.AspNetCore.Authorization;
using HH.API.Entity.BizModel;

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

        [HttpPost("AddBizProperty")]
        public JsonResult AddBizProperty([FromBody] BizProperty property)
        {
            // 数据格式校验
            JsonResult validateResult = null;
            if (!this.DataValidator<BizProperty>(property, out validateResult)) return validateResult;

            // 验证 SchemaCode 是否有效
            BizSchema bizSchema = this.bizSchemaRepository.GetBizSchemaByCode(property.SchemaCode);
            if (bizSchema == null)
            {
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.SchemaNotExists,
                    Message = "Schema is not exists"
                });
            }
            // 验证编码是否重复
            if (bizSchema.Properties.Exists((p) => { return p.PropertyCode == property.PropertyCode; }))
            {
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.CodeDuplicate,
                    Message = "Property code is exists."
                });
            }

            dynamic result = this.bizSchemaRepository.AddBizProperty(property);
            return Json(result);
        }

        public JsonResult AddBizSheet([FromBody]BizSheet bizSheet)
        {
            throw new NotImplementedException();
        }

        [HttpGet("AddWorkflowFolder")]
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
            // 根节点设置 ParentId 为null
            if (isRoot) parentId = null;

            #region 数据格式校验 ---------
            if (this.functionNodeRepository.GetFunctionNodeByName(FunctionType.WorkflowPackage, parentId, functionName) != null)
            {
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.NameDuplicate,
                    Message = "This name is already exists."
                });
            }
            if (!isRoot)
            {// TODO:判断ParentId是否存在
                FunctionNode parentNode = this.functionNodeRepository.GetObjectById(parentId);
                if (functionNode == null)
                {
                    return Json(new APIResult()
                    {
                        ResultCode = ResultCode.ParentNotExists,
                        Message = "Parent is not exists."
                    });
                }
            }
            #endregion

            dynamic res = this.functionNodeRepository.Insert(functionNode);

            return Json(res);
        }

        [HttpGet("GetRootFolders")]
        public JsonResult GetRootFolders()
        {
            List<FunctionNode> roots = this.functionNodeRepository.GetRootFunctionNodesByType(FunctionType.WorkflowPackage);
            return Json(roots);
        }

        [HttpGet("GetSubFolders")]
        public JsonResult GetSubFolders(string parentId)
        {
            List<FunctionNode> functionNodes = this.functionNodeRepository.GetSubFunctionNodesByParent(parentId);
            return Json(functionNodes);
        }

        /// <summary>
        /// 添加流程包
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="packageCode"></param>
        /// <param name="packageName"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [HttpGet("AddWorkflowPackage")]
        public JsonResult AddWorkflowPackage(string folderId,
            string packageCode,
            string packageName,
            int sortOrder)
        {
            #region 数据格式校验 -----------------------
            if (this.workflowPackageRepository.GetWorkflowPackageByCode(packageCode) != null)
            {// 编码已经存在
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.CodeDuplicate,
                    Message = "Workflow package code is already exists."
                });
            }
            #endregion

            // 新增 WorkflowPackage
            WorkflowPackage workflowPackage = new WorkflowPackage(folderId, packageCode, packageName, sortOrder);
            this.workflowPackageRepository.Insert(workflowPackage);

            // 新增 数据模型
            BizSchema schema = new BizSchema(packageCode, packageName, this.Authorization.ObjectId);
            this.bizSchemaRepository.Insert(schema);

            // 新增 默认表单
            BizSheet sheet = new BizSheet();
            sheet.Initial(packageCode, this.Authorization.ObjectId);
            if (this.bizSheetRepository.GetBizSheetByCode(sheet.SheetCode) == null)
            {
                this.bizSheetRepository.Insert(sheet);
            }
            else
            {
                this.LogWriter.Warn(string.Format("系统表单未自动创建，因为已经存在相同的表单编号：{0}",
                    sheet.SheetCode));
            }

            // 新增 默认流程
            WorkflowTemplate workflow = new WorkflowTemplate(packageCode,
                packageCode,
                packageName,
                this.Authorization.ObjectId,
                sortOrder);
            if (this.workflowTemplateRepository.GetDesignWorkflowTemplate(workflow.WorkflowCode) == null)
            {
                this.workflowTemplateRepository.Insert(workflow);
            }
            else
            {
                this.LogWriter.Warn(string.Format("流程模板未自动创建，因为已经存在相同的流程模板编号：{0}",
                    workflow.WorkflowCode));
            }

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
            WorkflowTemplate template = this.workflowTemplateRepository.GetWorkflowTemplate(workflowCode, workflowVersion);
            return Json(template);
        }

        public JsonResult GetDefaultWorkflowTemplate(string workflowCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult GetDesignerWorkflowTemplate(string workflowCode)
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetBizSchemaByCode")]
        public JsonResult GetBizSchemaByCode(string schemaCode)
        {
            BizSchema bizSchema = this.bizSchemaRepository.GetBizSchemaByCode(schemaCode);
            return Json(bizSchema);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="schemaCode">业务模型编码</param>
        /// <returns></returns>
        [HttpGet("PublishBizSchema")]
        public JsonResult PublishBizSchema(string schemaCode)
        {
            if (this.bizSchemaRepository.GetBizSchemaByCode(schemaCode) == null)
            {
                return Json(new APIResult()
                {
                    Message = "业务模型不存在",
                    ResultCode = ResultCode.SchemaNotExists
                });
            }
            bool res = this.bizSchemaRepository.PublishBizSchema(schemaCode);

            return Json(new APIResult()
            {
                ResultCode = res ? ResultCode.Success : ResultCode.Error
            });
        }

        public JsonResult RemoveBizProperty(string schemaCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult RemoveWorkflowPackage(string schemaCode)
        {
            throw new NotImplementedException();
        }

    }
}
