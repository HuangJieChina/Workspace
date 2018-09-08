using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Cache
{
    /// <summary>
    /// 缓存对象基类
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 获取缓存的数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取允许最大缓存数
        /// </summary>
        int MaxCacheSize { get; }
    }
}
