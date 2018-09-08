using HH.API.Entity.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Services.Admin
{
    public class ServiceRegister
    {
        private ServiceRegister() { }

        private static ServiceRegister _Instance = null;

        public static ServiceRegister Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ServiceRegister();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 获取当前服务所在的服务器IP地址
        /// </summary>
        public string CurrentIp { get; }

        /// <summary>
        /// 获取当前服务的端口号
        /// </summary>
        public int CurrentPort { get; }

        /// <summary>
        /// 获取或设置当前服务是否主服务
        /// </summary>
        public bool IsMaster { get; set; }

        /// <summary>
        /// 获取或设置所有服务列表
        /// </summary>
        public List<ServiceConfig> Services { get; set; }
    }
}