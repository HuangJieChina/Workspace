using HH.API.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Reflection;

namespace HH.API.Services
{
    public class ServiceFactory : ServiceBase
    {
        private ServiceFactory() { }

        public static object lockObject = new object();

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

        private IEnumerable<Type> _ImplementationTypes = null;
        /// <summary>
        /// 获取当前程序集的所有类
        /// </summary>
        public IEnumerable<Type> ImplementationTypes
        {
            get
            {
                if (this._ImplementationTypes == null)
                {
                    this._ImplementationTypes = this.GetType().Assembly.DefinedTypes.Where(type =>
                    type.IsClass && !type.IsAbstract && !type.IsGenericType && !type.IsNested);
                }
                return this._ImplementationTypes;
            }
        }

        /// <summary>
        /// 获取单例对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public T GetRepository<T>(string corpId)
        {
            string key = string.Format("{0}.{1}", typeof(T).FullName, corpId);

            try
            {
                Monitor.Enter(lockObject);

                if (services.ContainsKey(key))
                {
                    return (T)services[key];
                }

                T result = default(T);
                foreach (var type in this.ImplementationTypes)
                {
                    if (typeof(T).IsAssignableFrom(type))
                    {
                        object[] parameters = new object[1] { corpId };
                        // result = (T)type.Assembly.CreateInstance(type.FullName);
                        result = (T)type.Assembly.CreateInstance(type.FullName, true, BindingFlags.Default, null, parameters, null, null);
                        services.Add(key, result);
                        break;
                    }
                }

                return result;
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }


    }
}
