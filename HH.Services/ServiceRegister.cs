﻿using HH.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HH.API.Services
{
    /// <summary>
    /// 服务初始化
    /// </summary>
    public class ServiceRegister
    {
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private ServiceRegister()
        {

        }

        private static ServiceRegister _Instance = null;
        /// <summary>
        /// 获取对象实例
        /// </summary>
        public static ServiceRegister Instance
        {
            get
            {
                try
                {
                    Monitor.Enter(_Instance);

                    if (_Instance == null)
                    {
                        _Instance = new ServiceRegister();
                    }
                }
                finally
                {
                    Monitor.Exit(_Instance);
                }
                return _Instance;
            }
        }

        private static bool initialized = false;

        /// <summary>
        /// 服务初始化
        /// </summary>
        public void Initial()
        {
            VerifyLicense();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitialData()
        {
            try
            {
                Monitor.Enter(initialized);
                if (initialized) return;

                // TODO:服务数据初始化过程
                InitialFunctionNode();
                initialized = true;
            }
            finally
            {
                Monitor.Exit(initialized);
            }
        }

        /// <summary>
        /// 菜单节点初始化
        /// </summary>
        private void InitialFunctionNode()
        {
            //FunctionNodeRepository function = ServiceFactory.Instance.GetServices<FunctionNodeRepository>();
            //if (function.Count() == 0)
            //{
            //    FunctionNode packageRoot = new FunctionNode()
            //    {
            //        CreateBy = EntityConfig.Org.SystemUserId,
            //        DisplayName = EntityConfig.FunctionNode.WorkflowPackageRootName,
            //        IsRoot = true,
            //        FunctionType = FunctionType.WorkflowPackage
            //    };
            //}
        }

        /// <summary>
        /// 组织对象初始化
        /// </summary>
        private void InitialOrg()
        {
            OrgUnitRepository orgUnitRepository = ServiceFactory.Instance.GetServices<OrgUnitRepository>();
            OrgUserRepository orgUserRepository = ServiceFactory.Instance.GetServices<OrgUserRepository>();

            if (orgUnitRepository.Count() == 0)
            {
                OrgUnit rootUnit = new OrgUnit()
                {
                    ObjectId = EntityConfig.Org.SystemOrgId,
                    UnitName = "我的公司",
                    IsRootUnit = true,
                    IsEnabled = true
                };
                orgUnitRepository.Insert(rootUnit);

                OrgUser user = new OrgUser()
                {
                    ObjectId = EntityConfig.Org.SystemUserId,
                    Code = EntityConfig.Org.SystemAdministratorCode,
                    CnName = EntityConfig.Org.SystemAdministratorName,
                    IsAdministrator = true,
                    IsSystem = true
                };
                orgUserRepository.Insert(user);
            }
        }

        /// <summary>
        /// 验证注册码
        /// </summary>
        public void VerifyLicense()
        {
            int seconds = 10 * 1000; // 10秒钟
            System.Timers.Timer timer = new System.Timers.Timer(seconds);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        /// <summary>
        /// 注册定时器进行检查注册码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("注册码检查");
        }
    }
}