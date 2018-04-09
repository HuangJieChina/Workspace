using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.API.Entity.KeyCollectionCache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class Memory<T> : IKeyCollectionCache<T> where T : EntityBase
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

        private Dictionary<string, List<T>> memoryCache = new Dictionary<string, List<T>>();

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
        public List<T> Get(string key)
        {
            try
            {
                this.RWLock.AcquireReaderLock(-1);
                if (!this.memoryCache.ContainsKey(key)) return default(List<T>);
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

        public void Save(string key, T t)
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                if (!this.memoryCache.ContainsKey(key))
                {// 不存在则新增
                    this.memoryCache.Add(key, new List<T>());
                }
                if (this.memoryCache[key].Contains(t))
                {
                    this.memoryCache[key].Remove(t);
                }
                this.memoryCache[key].Add(t);
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }

        public void Remove(string key, T t)
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                if (!this.memoryCache.ContainsKey(key))
                {// 不存在则新增
                    return;
                }
                if (this.memoryCache[key].Contains(t))
                {
                    this.memoryCache[key].Remove(t);
                }
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }

    }
}
