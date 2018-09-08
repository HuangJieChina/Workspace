using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Services
{
    public class ServiceConfig
    {
        /// <summary>
        /// 获取或设置服务的IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 获取或设置服务的端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 获取或设置是否是主服务
        /// </summary>
        public bool IsMaster { get; set; }
    }
}
