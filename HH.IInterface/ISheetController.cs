using HH.API.Entity;
using HH.API.Entity.BizObject;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HH.IInterface
{
    /// <summary>
    /// 表单接口
    /// </summary>
    public interface ISheetController : IWorkflowRuntimeController
    {
        /// <summary>
        /// 加载表单数据
        /// </summary>
        /// <param name="workItemId"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult LoadData([FromHeader]string workItemId);

        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="xx"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult SaveData([FromBody]dynamic xx);
    }

    /// <summary>
    /// 返回前端的表单数据
    /// </summary>
    public class SheetData
    {
        /// <summary>
        /// 获取或设置业务流程实例数据
        /// </summary>
        public BizInstanceContext InstanceContext { get; set; }

        /// <summary>
        /// 获取或设置活动节点相关设置
        /// </summary>
        public ActivityTemplate ActivityTemplate { get; set; }

        /// <summary>
        /// 获取或设置任务Id
        /// </summary>
        public string WorkItemId { get; set; }
    }
}
