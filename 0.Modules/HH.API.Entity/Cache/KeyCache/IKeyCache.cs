﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.API.Entity.Cache.KeyCache
{
    /// <summary>
    /// KeyValue缓存接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKeyCache<T> : ICache
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
        void Remove(string key);

        /// <summary>
        /// 根据Key值获取一个对象
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns></returns>
        T Get(string key);

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsKey(string key);


    }
}