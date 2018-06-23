using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity
{
    /// <summary>
    /// Token存储的对象
    /// </summary>
    public class SsoToken
    {
        /// <summary>
        /// 获取或设置Token创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 获取或设置过期时间
        /// </summary>
        public int ExpireSecond { get; set; }
        /// <summary>
        /// 获取或设置加密的值
        /// </summary>
        public string TokenValue { get; set; }
        /// <summary>
        /// 获取或设置来源的CorpId
        /// </summary>
        public string SourceCorpId { get; set; }
        /// <summary>
        /// 获取或设置目标系统的CorpId
        /// </summary>
        public string TargetCorpId { get; set; }
    }
}
