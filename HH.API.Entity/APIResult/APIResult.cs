using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity
{
    /// <summary>
    /// API接口统一返回类
    /// </summary>
    public class APIResult
    {
        /// <summary>
        /// 获取或设置是否成功
        /// </summary>
        public bool Successful
        {
            get
            {
                return ResultCode == ResultCode.Success;
            }
        }

        /// <summary>
        /// 获取或设置返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置的返回编码
        /// </summary>
        public ResultCode ResultCode { get; set; }

        /// <summary>
        /// 获取或设置扩展信息
        /// </summary>
        public object Extend { get; set; }
    }
}
