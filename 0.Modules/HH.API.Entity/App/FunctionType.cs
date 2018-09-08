using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.App
{
    /// <summary>
    /// 功能菜单类型
    /// </summary>
    public enum FunctionType
    {
        /// <summary>
        /// 文件目录
        /// </summary>
        Folder = 0,
        /// <summary>
        /// URL链接
        /// </summary>
        Url = 1,
        /// <summary>
        /// 业务模型
        /// </summary>
        BizModel = 2,
        /// <summary>
        /// 报表
        /// </summary>
        Report = 3
    }
}
