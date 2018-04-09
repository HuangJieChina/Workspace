using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.API.Entity.EntityCache
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class Redis<T> : IEntityCache<T> where T : EntityBase
    {
        public int Count => throw new NotImplementedException();

        public int MaxCacheSize => throw new NotImplementedException();

        public void AddRange(List<T> array)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public T Get(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Save(T t)
        {
            throw new NotImplementedException();
        }
    }
}
