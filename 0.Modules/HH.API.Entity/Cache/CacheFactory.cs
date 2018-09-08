using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Cache
{
    public class CacheFactory : ICacheFactory
    {
        /// <summary>
        /// 本地存储的缓存对象
        /// </summary>
        protected Dictionary<string, ICache> Caches
        {
            get; set;
        } = new Dictionary<string, ICache>();

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            foreach (string key in this.Caches.Keys)
            {
                this.Caches[key].Clear();
            }
        }

        /// <summary>
        /// 获取所有缓存的存储长度
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetCachesLength()
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            foreach (string key in this.Caches.Keys)
            {
                results.Add(key, this.Caches[key].Count);
            }
            return results;
        }


    }
}
