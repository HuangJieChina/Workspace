using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.API.Entity.Cache.ObjectCache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class Memory<T> : IListCache<T>
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

        private List<T> memoryCache = new List<T>();

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


        public void Add(T t)
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                this.memoryCache.Add(t);
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }

        public void Add(List<T> values)
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                this.memoryCache.AddRange(values);
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }

        public void RemoveAt(int index)
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                this.memoryCache.RemoveAt(index);
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }



        public bool Contains(T t)
        {
            try
            {
                this.RWLock.AcquireReaderLock(-1);
                return this.memoryCache.Contains(t);
            }
            finally { this.RWLock.ReleaseReaderLock(); }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="t"></param>
        public void Remove(T t)
        {
            try
            {
                this.RWLock.AcquireWriterLock(-1);
                this.memoryCache.Remove(t);
            }
            finally { this.RWLock.ReleaseWriterLock(); }
        }

    }
}
