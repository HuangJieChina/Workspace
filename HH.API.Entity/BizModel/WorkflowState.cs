using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.BizModel
{
    /// <summary>
    /// 流程模板状态
    /// </summary>
    public enum WorkflowState
    {
        /// <summary>
        /// 设计版本
        /// </summary>
        Design = 0,
        /// <summary>
        /// 已发布的
        /// </summary>
        Published = 1,
        /// <summary>
        /// 已作废的
        /// </summary>
        Discarded = 2
    }

}
