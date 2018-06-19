using HH.API.Entity;
using HH.API.Entity.BizObject;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HH.IInterface
{
    /// <summary>
    /// 业务数据运行
    /// </summary>
    public interface IBizObjectRuntimeController : IBaseController
    {
        #region 业务数据接口 -----------------
        /// <summary>
        /// 获取业务数据接口
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet]
        BizObject GetBizObject([FromHeader]string schemaCode, [FromHeader]string objectId);

        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bizObject"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult SaveBizObject([FromBody]BizObject bizObject);

        /// <summary>
        /// 移除业务数据
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult RemoveBizObject([FromHeader]string schemaCode, [FromHeader]string objectId);

        /// <summary>
        /// 获取业务列表的数据
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="conditions"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult GetBizObjectList(string schemaCode, dynamic conditions, dynamic sort, int pageIndex, int pageSize);

        /// <summary>
        /// 执行业务操作后返回业务对象
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="objectId"></param>
        /// <param name="methodeCode"></param>
        /// <returns></returns>
        [HttpGet]
        BizObject ExecuteAction(string schemaCode, string objectId, string methodeCode);
        #endregion
    }

    /// <summary>
    /// 流程实例运行
    /// </summary>
    public interface IWorkflowRuntimeController : IBizObjectRuntimeController
    {
        #region 流程实例接口 ------------------
        /// <summary>
        /// 启动业务流程实例
        /// </summary>
        /// <param name="parameters">业务流程实例Id</param>
        /// <returns></returns>
        [HttpPost]
        JsonResult StartWorkflow([FromBody]dynamic parameters);

        /// <summary>
        /// 终止业务流程
        /// </summary>
        /// <param name="instanceId">业务流程实例Id</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult AbortWorkflow([FromHeader]string instanceId);

        /// <summary>
        /// 取消业务流程
        /// </summary>
        /// <param name="instanceId">业务流程实例Id</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult CancelWorkflow([FromHeader]string instanceId);
        #endregion

        #region 流程运行接口 ------------------
        /// <summary>
        /// 获取工作任务
        /// </summary>
        /// <param name="objectId">任务Id</param>
        /// <returns></returns>
        [HttpGet]
        BizWorkItem GetWorkItem(string objectId);

        /// <summary>
        /// 结束传阅任务
        /// </summary>
        /// <param name="circulateItemId"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult FinishCirculateItem([FromBody]string circulateItemId);

        /// <summary>
        /// 批量设置已阅
        /// </summary>
        /// <param name="circulateItemIds"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult FinishCirculateItem([FromBody]List<string> circulateItemIds);

        /// <summary>
        /// 转发操作
        /// </summary>
        /// <param name="circulateItemId"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult ForwardWorkItem([FromBody]string circulateItemId);

        /// <summary>
        /// 提交消息至后端服务(流程/任务状态变化通过接口执行，流程跳转通过消息异步执行)
        /// </summary>
        /// <param name="workflowMessage">消息体</param>
        /// <returns></returns>
        [HttpPost]
        JsonResult SendMessage([FromBody]WorkflowMessage workflowMessage);
        #endregion

        #region 流程数据接口 ------------------
        /// <summary>
        /// 获取流程实例数据
        /// </summary>
        /// <param name="instanceId">流程实例数据</param>
        /// <returns></returns>
        [HttpPost]
        BizInstanceContext GetInstanceContext([FromHeader]string instanceId);


        #endregion
    }
}