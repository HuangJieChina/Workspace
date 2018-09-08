using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.BizModel
{
    /// <summary>
    /// 活动节点类型
    /// </summary>
    public enum ActivityType
    {
        /// <summary>
        /// 用户活动(发起、审批)
        /// </summary>
        User = 0,
        /// <summary>
        /// 系统活动(执行业务事件，只调用业务服务实现)
        /// </summary>
        System = 1,
        /// <summary>
        /// 连接点(不执行任何操作)
        /// </summary>
        Connect = 2,
        /// <summary>
        /// 传阅活动(传阅任务)
        /// </summary>
        Circulate = 3,
        /// <summary>
        /// 子流程
        /// </summary>
        SubWorkflow = 4
    }
}
