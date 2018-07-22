using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.API.Entity.Cache.KeyCollectionCache
{
    /// <summary>
    /// KeyValue 缓存工厂类
    /// </summary>
    public class KeyCacheFactory<T> 
    {
        private KeyCacheFactory() { }

        private static KeyCacheFactory<T> _Instance = null;
        public static KeyCacheFactory<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new KeyCacheFactory<T>();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <returns></returns>
        public IKeyCache<T> GetCache()
        {
            return this.GetCache(int.MaxValue);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="maxCacheSize"></param>
        /// <returns></returns>
        public IKeyCache<T> GetCache(int maxCacheSize)
        {
            return new Memory<T>(maxCacheSize);
        }
    }
}
