using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.API.Entity
{
    /// <summary>
    /// 实体对象缓存接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKeyCollectionCache<T> where T : EntityBase
    {
        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="t"></param>
        void Save(string key, T t);

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key, T t);

        /// <summary>
        /// 根据Key值移除对象
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 根据Key值获取一个对象
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns></returns>
        List<T> Get(string key);

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 清空所有缓存数据
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取缓存数据量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取允许最大缓存数
        /// </summary>
        int MaxCacheSize { get; }

    }
}