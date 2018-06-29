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
        Error = -1,

        /// <summary>
        /// 实体数据格式校验错误
        /// </summary>
        DataFromatError = 1,

        /// <summary>
        /// 名称重复
        /// </summary>
        NameDuplicate = 2,
        /// <summary>
        /// 业务模型不存在
        /// </summary>
        SchemaNotExists = 3,

        #region 组织机构服务以10开头
        /// <summary>
        /// 编码重复
        /// </summary>
        CodeDuplicate = 100001,
        /// <summary>
        /// 父组织不存在
        /// </summary>
        ParentNotExists = 100002,
        /// <summary>
        /// 相同名称或编码组织已经存在
        /// </summary>
        UnitIsExists = 100003,
        /// <summary>
        /// 名称不允许为空
        /// </summary>
        NameIsEmpty = 100004
        #endregion

    }
}
