using HH.API.Entity;
using HH.API.Entity.BizObject;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HH.API.IController
{
    /// <summary>
    /// 组织机构服务接口类
    /// </summary>
    public interface IWorkflowManagerController : IBaseController
    {
        #region 流程目录接口 ------------------
        /// <summary>
        /// 增加流程目录
        /// </summary>
        /// <param name="functionNode"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddWorkflowFolder([FromBody]FunctionNode functionNode);
        #endregion

        #region 流程包接口 --------------------
        /// <summary>
        /// 增加流程包
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="packageCode"></param>
        /// <param name="packageName"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddWorkflowPackage([FromHeader]string folderId,
            [FromHeader]string packageCode,
            [FromHeader]string packageName,
            [FromHeader]int sortOrder);

        /// <summary>
        /// 移除流程包
        /// </summary>
        /// <param name="schemaCode">业务模型编码</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult RemoveWorkflowPackage([FromHeader]string schemaCode);
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
        JsonResult AddWorkflowTemplate([FromHeader]string schemaCode,
            [FromHeader]string workflowCode,
            [FromHeader]string displayName,
            [FromHeader]int sortOrder);
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
        BizSchema GetBizSchemaByCode(string schemaCode);

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
        JsonResult RemoveBizProperty([FromHeader]string schemaCode);

        /// <summary>
        /// 发布数据模型
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult PublishBizSchema([FromHeader]string schemaCode);

        #endregion
    }
}
