using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Message
{
    /// <summary>
    /// 业务流程消息对象
    /// </summary>
    public class WorkflowMessage
    {
        /// <summary>
        /// 获取或设置流程实例Id
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// 获取或设置任务Id
        /// </summary>
        public string WorkItemId { get; set; }

        /// <summary>
        /// 获取或设置是否审批同意
        /// </summary>
        public bool Approval { get; set; }

        /// <summary>
        /// 获取或设置提交的目标节点
        /// </summary>
        public string TargetActivity { get; set; }

        /// <summary>
        /// 获取或设置激活节点的指定参与者(与TargetActivity一起配合使用)
        /// </summary>
        public List<string> Participants { get; set; }

    }


}
