using HH.API.Entity;
using HH.API.Entity.Orgainzation;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HH.API.Services
{
    /// <summary>
    /// 服务初始化
    /// </summary>
    public class ServiceInit
    {
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private ServiceInit()
        { }

        private static ServiceInit _Instance = null;
        /// <summary>
        /// 获取对象实例
        /// </summary>
        public static ServiceInit Instance
        {
            get
            {
                try
                {
                    Monitor.Enter(lockObject);

                    if (_Instance == null)
                    {
                        _Instance = new ServiceInit();
                    }
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
                return _Instance;
            }
        }

        private static bool initialized = false;

        private readonly static object lockObject = new object();

        /// <summary>
        /// 服务启动并初始化
        /// </summary>
        public void Initial()
        {
            try
            {
                if (initialized) return;

                Monitor.Enter(lockObject);

                initialized = true;

                // 主体操作开始 -------
                this.InitialData();
                // End
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitialData()
        {
            // 校验注册码
            VerifyLicense();
            // TODO:服务数据初始化过程
            InitialFunctionNode();
            // 初始化组织
            InitialOrg();
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
            OrgDepartmentRepository orgUnitRepository = new OrgDepartmentRepository();
            OrgUserRepository orgUserRepository = new OrgUserRepository();

            if (orgUnitRepository.Count() == 0)
            {
                OrgDepartment rootUnit = new OrgDepartment()
                {
                    ObjectId = EntityConfig.Org.SystemOrgId,
                    DisplayName = "我的公司",
                    IsRootUnit = true,
                    IsEnabled = true
                };
                orgUnitRepository.Insert(rootUnit);
            }

            if (orgUserRepository.Count() == 0)
            {
                OrgUser user = new OrgUser()
                {
                    ObjectId = EntityConfig.Org.SystemUserId,
                    Code = EntityConfig.Org.SystemAdministratorCode,
                    DisplayName = EntityConfig.Org.SystemAdministratorName,
                    IsAdministrator = true,
                    IsSystemUser = true
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
            LogManager.GetCurrentClassLogger().Error("注册码检查");
            // Console.WriteLine("注册码检查");
        }
    }
}