using HH.API.Entity;
using HH.API.Entity.BizObject;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HH.API.IController
{
    /// <summary>
    /// 表单接口
    /// </summary>
    public interface ISheetController : IWorkflowRuntimeController
    {
        /// <summary>
        /// 加载表单数据
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult LoadData(dynamic inputs);

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

    /// <summary>
    /// 表单模式
    /// </summary>
    public enum SheetMode
    {
        /// <summary>
        /// 发起模式
        /// </summary>
        Originate = 0,
        /// <summary>
        /// 查看模式
        /// </summary>
        View = 1,
        /// <summary>
        /// 工作模式
        /// </summary>
        Work = 2,
        /// <summary>
        /// 管理员模式(可修改数据)
        /// </summary>
        Admin = 3
    }
}
