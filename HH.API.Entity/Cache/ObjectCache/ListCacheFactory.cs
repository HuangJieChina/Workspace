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
    public class ListCacheFactory<T>
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
        /// <returns></returns>
        public IListCache<T> GetCache()
        {
            return this.GetCache(int.MaxValue);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="maxCacheSize"></param>
        /// <returns></returns>
        public IListCache<T> GetCache(int maxCacheSize)
        {
            return new Memory<T>(maxCacheSize);
        }
    }
}
