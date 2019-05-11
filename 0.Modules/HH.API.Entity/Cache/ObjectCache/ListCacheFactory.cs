using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.API.Entity.Cache.ObjectCache
{
    /// <summary>
    /// KeyValue 缓存工厂类
    /// </summary>
    public class ListCacheFactory<T> : CacheFactory
    {
        private ListCacheFactory() { }

        private static ListCacheFactory<T> _Instance = null;
        public static ListCacheFactory<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ListCacheFactory<T>();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="cacheKey">缓存对象Key值(唯一标识)</param>
        /// <returns></returns>
        public IListCache<T> GetCache( string cacheKey)
        {
            return this.GetCache(cacheKey, int.MaxValue);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="cacheKey">缓存对象Key值(唯一标识)</param>
        /// <param name="maxCacheSize"></param>
        /// <returns></returns>
        public IListCache<T> GetCache(string cacheKey, int maxCacheSize)
        {
            Memory<T> cache = null;
            try
            {
                Monitor.Enter(_Instance);

                if (this.Caches.ContainsKey(cacheKey))
                {
                    throw new Exception("Get key cache error,this key is aleardy exists:" + cacheKey);
                }
                cache = new Memory<T>(maxCacheSize);
                this.Caches.Add(cacheKey, (ICache)cache);
            }
            finally
            {
                Monitor.Exit(_Instance);
            }
            return cache;
        }

        // End 
    }
}
