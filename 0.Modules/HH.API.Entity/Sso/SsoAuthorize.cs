using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Sso
{
    /// <summary>
    /// SSO接口授权
    /// </summary>
    [Serializable]
    public class SsoAuthorize : EntityBase
    {
        public SsoAuthorize() { }

        /// <summary>
        /// 获取或设置被授权的系统编码
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// 获取或设置被授权的开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置被授权的结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 获取或设置被授权的API接口名称
        /// </summary>
        public string APIName { get; set; }

        /// <summary>
        /// 获取或设置被授权的方法名称
        /// </summary>
        public string MethodName { get; set; }

        public override string TableName => throw new NotImplementedException();
    }
}