using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HH.API.Services
{
    public class ServiceFactory : ServiceBase
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
                    Monitor.Enter(lockObject);
                    if (_Instance == null)
                    {
                        _Instance = new ServiceFactory();
                    }
                }
                finally
                {
                    Monitor.Exit(lockObject);
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
                Monitor.Enter(lockObject);

                if (services.ContainsKey(fullName))
                {
                    return services[fullName] as T;
                }
                // 初始化数据，License验证
                ServiceRegister.Instance.Initial();

                T t = new T();
                services.Add(fullName, t);
                return t;
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }
    }
}
