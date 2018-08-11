using HH.API.Entity;
using HH.API.Entity.BizModel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HH.API.IController
{
    /// <summary>
    /// 组织机构服务接口类
    /// </summary>
    public interface IBizModelController : IBaseController
    {
        #region 流程包接口 --------------------
        /// <summary>
        /// 新增业务模型
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="packageCode"></param>
        /// <param name="packageName"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddBizPackage(
            string folderId,
            string packageCode,
            string packageName,
            int sortOrder);

        /// <summary>
        /// 移除业务模型
        /// </summary>
        /// <param name="userId">操作用户</param>
        /// <param name="schemaCode">业务模型编码</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult RemoveBizPackage(string schemaCode);
        #endregion

        #region 业务流程接口 ------------------
        /// <summary>
        /// 增加业务流程模板
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="workflowCode"></param>
        /// <param name="displayName"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddWorkflowTemplate(string schemaCode,
            string workflowCode,
            string displayName,
            int sortOrder);

        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <param name="workflowVersion"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetWorkflowTemplate(string workflowCode, int workflowVersion);

        /// <summary>
        /// 获取最新发布版的流程模板
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetDefaultWorkflowTemplate(string workflowCode);

        /// <summary>
        /// 获取设计中的流程模板
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetDesignerWorkflowTemplate(string workflowCode);
        #endregion

        #region 业务表单接口 ------------------
        /// <summary>
        /// 增加业务表单
        /// </summary>
        /// <param name="bizSheet"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddBizSheet([FromBody]BizSheet bizSheet);
        #endregion

        #region 数据模型接口 ------------------
        /// <summary>
        /// 根据业务模型编码获取数据模型
        /// </summary>
        /// <param name="schemaCode">业务模型编码</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetBizSchemaByCode(string schemaCode);

        /// <summary>
        /// 增加业务属性
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddBizProperty([FromBody]BizProperty property);

        /// <summary>
        /// 增加业务属性
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult RemoveBizProperty(string schemaCode);

        /// <summary>
        /// 发布数据模型
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult PublishBizSchema(string schemaCode);

        #endregion
    }
}
