﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HH.API.Services
{
    public class ServiceFactory
    {
        private ServiceFactory() { }

        private static ServiceFactory _Instance = null;
        /// <summary>
        /// 获取单例对象实例
        /// </summary>
        public static ServiceFactory Instance
        {
            get
            {
                try
                {
                    Monitor.Enter(_Instance);
                    if (_Instance == null)
                    {
                        _Instance = new ServiceFactory();
                    }
                }
                finally
                {
                    Monitor.Exit(_Instance);
                }
                return _Instance;
            }
        }

        private Dictionary<string, object> services = new Dictionary<string, object>();

        /// <summary>
        /// 获取单例对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetServices<T>() where T : class, new()
        {
            string fullName = typeof(T).FullName;

            try
            {
                Monitor.Enter(_Instance);

                if (services.ContainsKey(fullName))
                {
                    return services[fullName] as T;
                }

                T t = new T();
                services.Add(fullName, t);
                return t;
            }
            finally
            {
                Monitor.Exit(_Instance);
            }
        }
    }
}
