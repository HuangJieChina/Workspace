using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API
{
    /// <summary>
    /// API接口返回统一编码
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 返回成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 返回未知错误
        /// </summary>
        Undefined = -1,

        #region 组织机构服务以10开头
        /// <summary>
        /// 编码重复
        /// </summary>
        CodeDuplicate = 1000001
        #endregion

    }
}
