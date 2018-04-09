using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.API.Entity.KeyCollectionCache
{
    /// <summary>
    /// 缓存工厂类
    /// </summary>
    public class Factory<T> where T : EntityBase
    {
        private Factory() { }

        private static Factory<T> _Instance = null;
        public static Factory<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Factory<T>();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <returns></returns>
        public IKeyCollectionCache<T> GetCache()
        {
            return new Memory<T>();
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="maxCacheSize"></param>
        /// <returns></returns>
        public IKeyCollectionCache<T> GetCache(int maxCacheSize)
        {
            return new Memory<T>(maxCacheSize);
        }
    }
}
