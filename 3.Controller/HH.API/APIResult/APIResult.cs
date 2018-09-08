using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API
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

        private string _Message = null;

        /// <summary>
        /// 获取或设置返回消息
        /// </summary>
        public string Message
        {
            get
            {
                if (this._Message == null)
                {
                    this._Message = this.ResultCode.ToString();
                }
                return this._Message;
            }
            set { this._Message = value; }
        }

        private ResultCode _ResultCode = ResultCode.Success;
        /// <summary>
        /// 获取或设置的返回编码
        /// </summary>
        public ResultCode ResultCode
        {
            get { return this._ResultCode; }
            set { this._ResultCode = value; }
        }

        /// <summary>
        /// 获取或设置扩展信息
        /// </summary>
        public object Extend { get; set; }
    }
}