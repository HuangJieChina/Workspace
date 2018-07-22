using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Cache.ObjectCache
{
    public interface IListCache<T>
    {
        /// <summary>
        /// 新增缓存对象
        /// </summary>
        /// <param name="t"></param>
        void Add(T t);

        /// <summary>
        /// 批量添加对象
        /// </summary>
        /// <param name="values"></param>
        void Add(List<T> values);

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key"></param>
        void Remove(T t);

        /// <summary>
        /// 从指定索引位置移除
        /// </summary>
        /// <param name="index"></param>
        void RemoveAt(int index);

        /// <summary>
        /// 是否存在值
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Contains(T t);

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