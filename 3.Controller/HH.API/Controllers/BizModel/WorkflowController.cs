﻿using System;
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
    public class WorkflowController : APIController, IBizModelController
    {
        #region 服务注入对象 --------------------
        /// <summary>
        /// 流程包
        /// </summary>
        private IBizPackageRepository workflowPackageRepository = null;
        private IBizSchemaRepository bizSchemaRepository = null;
        private IBizSheetRepository bizSheetRepository = null;
        private IWorkflowTemplateRepository workflowTemplateRepository = null;
        public IAppFunctionRepository functionNodeRepository = null;
        #endregion

        public WorkflowController(IBizPackageRepository workflowPackageRepository,
            IBizSchemaRepository bizSchemaRepository,
            IBizSheetRepository bizSheetRepository,
            IWorkflowTemplateRepository workflowTemplateRepository,
            IAppFunctionRepository functionNodeRepository)
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
            // TODO:权限校验

            // 数据格式校验
            JsonResult validateResult = null;
            if (!this.DataValidator<BizProperty>(property, out validateResult)) return validateResult;

            // 验证 SchemaCode 是否有效
            BizSchema bizSchema = this.bizSchemaRepository.GetBizSchemaByCode(property.SchemaCode);
            if (bizSchema == null)
            {
                return Json(new APIResult()
                {
                    ResultCode = APIResultCode.SchemaNotExists,
                    Message = "Schema is not exists"
                });
            }
            // 验证编码是否重复
            if (bizSchema.Properties.Exists((p) => { return p.PropertyCode == property.PropertyCode; }))
            {
                return Json(new APIResult()
                {
                    ResultCode = APIResultCode.CodeDuplicate,
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

        /// <summary>
        /// 添加流程包
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="packageCode"></param>
        /// <param name="packageName"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [HttpGet("AddBizPackage")]
        public JsonResult AddBizPackage(dynamic bizPackage)
        {
            string userId = bizPackage.userId;
            string folderId = bizPackage.folderId;
            string packageCode = bizPackage.packageCode;
            string packageName = bizPackage.packageName;
            int sortOrder = bizPackage.sortOrder;

            return MonitorFunction("AddWorkflowPackage", () =>
            {
                #region 数据格式校验 -----------------------
                if (this.workflowPackageRepository.GetBizPackageByCode(packageCode) != null)
                {// 编码已经存在
                    return Json(new APIResult()
                    {
                        ResultCode = APIResultCode.CodeDuplicate,
                        Message = "Workflow package code is already exists."
                    });
                }
                #endregion

                // 新增 业务模型
                BizPackage workflowPackage = new BizPackage(folderId, packageCode, packageName, sortOrder);
                this.workflowPackageRepository.Insert(workflowPackage);

                // 新增 数据模型
                BizSchema schema = new BizSchema(packageCode, packageName, userId);
                this.bizSchemaRepository.Insert(schema);

                // 新增 默认表单
                BizSheet sheet = new BizSheet();
                sheet.Initial(packageCode, userId);
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
                    userId,
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

                return Json(new APIResult() { Message = "OK", ResultCode = APIResultCode.Success });
            });
        }

        /// <summary>
        /// 新增流程模板
        /// </summary>
        /// <param name="workflowTemplate"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AddWorkflowTemplate(dynamic workflowTemplate)
        {
            string schemaCode = workflowTemplate.schemaCode;
            string userId = workflowTemplate.userId;
            string workflowCode = workflowTemplate.workflowCode;
            string displayName = workflowTemplate.displayName;
            int sortOrder = workflowTemplate.sortOrder;

            return MonitorFunction("AddWorkflowTemplate", () =>
            {
                // 校验流程模板编码是否已经存在
                if (this.workflowTemplateRepository.GetDesignWorkflowTemplate(workflowCode) != null)
                {
                    return Json(APIResultCode.CodeDuplicate, "Workflow code is already exists!");
                }

                WorkflowTemplate workflow = new WorkflowTemplate(schemaCode,
                    workflowCode,
                    displayName,
                    userId,
                    sortOrder);
                this.workflowTemplateRepository.Insert(workflow);

                return Json(new APIResult() { Message = "OK", ResultCode = APIResultCode.Success });
            });
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
                    ResultCode = APIResultCode.SchemaNotExists
                });
            }
            bool res = this.bizSchemaRepository.PublishBizSchema(schemaCode);

            return Json(new APIResult()
            {
                ResultCode = res ? APIResultCode.Success : APIResultCode.Error
            });
        }

        public JsonResult RemoveBizProperty(string schemaCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult RemoveBizPackage(string schemaCode)
        {
            throw new NotImplementedException();
        }
    }
}
