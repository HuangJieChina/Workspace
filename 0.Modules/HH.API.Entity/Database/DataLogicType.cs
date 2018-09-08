using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Database
{
    /// <summary>
    /// 数据项的业务逻辑类型
    /// </summary>
    public enum DataLogicType
    {
        /// <summary>
        /// 短文本类型
        /// </summary>
        ShortString = 0,
        /// <summary>
        /// 长文本类型
        /// </summary>
        LongString = 1,
        /// <summary>
        /// Html类型
        /// </summary>
        Html = 2,
        /// <summary>
        /// 整数类型
        /// </summary>
        Int = 3,
        /// <summary>
        /// 数值类型
        /// </summary>
        Number = 4,
        /// <summary>
        /// 日期类型
        /// </summary>
        DateTime = 5,
        /// <summary>
        /// 附件类型
        /// </summary>
        Attachment = 6
    }
}