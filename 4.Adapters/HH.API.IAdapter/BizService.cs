using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.IAdapters
{
    public class BizService
    {
        /// <summary>
        /// 获取或设置服务编码
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 适配器编码
        /// </summary>
        public string AdapterCode { get; set; }

        /// <summary>
        /// 获取或设置适配器信息
        /// </summary>
        public IAdapter Adapter { get; set; }
    }
}