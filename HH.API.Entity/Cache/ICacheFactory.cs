using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Cache
{
    /// <summary>
    /// 缓存对象基类
    /// </summary>
    public interface ICacheFactory
    {
        /// <summary>
        /// 获取缓存的数量
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> GetCachesLength();

        /// <summary>
        /// 清空全部的缓存
        /// </summary>
        void Clear();

    }
}
