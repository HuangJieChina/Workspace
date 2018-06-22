using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.API.Entity.EntityCache
{
    /// <summary>
    /// 实体对象缓存工厂类
    /// </summary>
    public class EntityCacheFactory<T> where T : EntityBase
    {
        private EntityCacheFactory() { }

        private static EntityCacheFactory<T> _Instance = null;
        public static EntityCacheFactory<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new EntityCacheFactory<T>();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <returns></returns>
        public IEntityCache<T> GetCache()
        {
            return this.GetCache(int.MaxValue);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="maxCacheSize"></param>
        /// <returns></returns>
        public IEntityCache<T> GetCache(int maxCacheSize)
        {
            return new Memory<T>(maxCacheSize);
        }
    }
}
