using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.API.Entity.Cache.EntityCache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class Memory<T> : IEntityCache<T> where T : EntityBase
    {
        /// <summary>
        /// 设置默认缓存数据量大小
        /// </summary>
        private const int DefaultMaxCacheSize = 99999;

        public Memory() : this(DefaultMaxCacheSize)
        {
        }

        public Memory(int maxCacheSize)
        {
            this._MaxCacheSize = maxCacheSize;
        }

        private Dictionary<string, T> memoryCache = new Dictionary<string, T>();

        private ReaderWriterLock _rwLock = null;
        /// <summary>
        /// 获取读写锁对象
        /// </summary>
        protected ReaderWriterLock RWLock
        {
            get
            {
                if (this._rwLock == null)
                {
                    this._rwLock = new ReaderWriterLock();
                }
                return this._rwLock;
            }
        }

        /// <summary>
        /// 缓存数据量大小
        /// </summary>
        public int Count
        {
            get
            {
                try
                {
                    this.RWLock.AcquireReaderLock(-1);
                    return this.memoryCache.Count;
                }
                finally
                {
                    this.RWLock.ReleaseReaderLock();
                }
            }
        }

        public T Get(string key)
        {
            try
            {
                this.RWLock.AcquireReaderLock(-1);
                if (!this.memoryCache.ContainsKey(key)) return default(T);
                return this.memoryCache[key];
            }
            finally { this.RWLock.ReleaseReaderLock(); }
        }

        private int _MaxCacheSize = 0;
        public int MaxCacheSize
        {
            get
            {
                return this._MaxCacheSize;
            }
        }

        public void Save(T t)
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                if (!this.memoryCache.ContainsKey(t.ObjectId))
                {// 不存在则新增
                    this.memoryCache.Add(t.ObjectId, t);
                }
                else
                {// 存在则更新
                    this.memoryCache[t.ObjectId] = t;
                }
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }

        /// <summary>
        /// 往缓存中增加数组
        /// </summary>
        /// <param name="array"></param>
        public void AddRange(List<T> array)
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                foreach (T t in array)
                {
                    if (!this.memoryCache.ContainsKey(t.ObjectId))
                    {// 不存在则新增
                        this.memoryCache.Add(t.ObjectId, t);
                    }
                    else
                    {// 存在则更新
                        this.memoryCache[t.ObjectId] = t;
                    }
                }
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }

        public void Clear()
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                this.memoryCache.Clear();
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }

        public bool Exists(string key)
        {
            try
            {
                this.RWLock.AcquireReaderLock(-1);
                return this.memoryCache.ContainsKey(key);
            }
            finally { this.RWLock.ReleaseReaderLock(); }
        }

        public void Remove(string key)
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                if (this.memoryCache.ContainsKey(key))
                {
                    this.memoryCache.Remove(key);
                }
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }


    }
}
