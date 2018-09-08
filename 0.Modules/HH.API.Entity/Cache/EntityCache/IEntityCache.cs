using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.API.Entity.Cache.EntityCache
{
    /// <summary>
    /// 实体对象缓存接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityCache<T> : ICache where T : EntityBase
    {
        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="t">缓存对象</param>
        void Save(T t);

        /// <summary>
        /// 将集合增加到缓存中
        /// </summary>
        /// <param name="array"></param>
        void AddRange(List<T> array);

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 根据Key值获取一个对象
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>实体对象</returns>
        T Get(string key);

        /// <summary>
        /// 检测是否存在
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>检查结果</returns>
        bool Exists(string key);

    }
}