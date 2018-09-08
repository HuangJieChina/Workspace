using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.IAdapters
{
    public interface IAdapter
    {
        /// <summary>
        /// 获取业务方法
        /// </summary>
        /// <returns></returns>
        List<BizMethod> LoadMethods();

        /// <summary>
        /// 执行业务服务方法
        /// </summary>
        /// <returns></returns>
        List<BizProperty> InvokeMethod();

        /// <summary>
        /// 获取或设置属性信息
        /// </summary>
        Dictionary<string, object> Properties { get; set; }

        /// <summary>
        /// 获取或设置方法
        /// </summary>
        List<BizMethod> Methods { get; set; }
    }
}