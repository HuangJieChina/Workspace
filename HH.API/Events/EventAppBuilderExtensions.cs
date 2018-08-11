using HH.API.Entity.App;
using HH.API.Entity.Orgainzation;
using HH.API.IServices;
using HH.API.Services;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HH.API.Events
{
    /// <summary>
    /// 事件注册
    /// </summary>
    public static class EventAppBuilderExtensions
    {
        public static IApplicationBuilder UseRegisterEvent(this IApplicationBuilder app)
        {
            // 事件注册
            RegisterEvent();

            return app;
        }

        public static IEntityEventBus entityEventBus { get; }
            = ServiceFactory.Instance.GetRepository<IEntityEventBus>();

        /// <summary>
        /// 事件注册
        /// </summary>
        private static void RegisterEvent()
        {
            entityEventBus.RegisterAfterInsertEvent<OrgUser>((user, result) =>
            {
                NLog.LogManager.GetCurrentClassLogger().Debug("RegisterEntityEvent OrgUser->" + result);
            });

            entityEventBus.RegisterAfterInsertEvent<AppPackage>((package, result) =>
            {
                NLog.LogManager.GetCurrentClassLogger().Debug("RegisterEntityEvent AppPackage1.Start->" + result);
                Thread.Sleep(3 * 1000);
                NLog.LogManager.GetCurrentClassLogger().Debug("RegisterEntityEvent AppPackage1.End->" + result);
            });

            entityEventBus.RegisterAfterInsertEvent<AppPackage>((package, result) =>
            {
                NLog.LogManager.GetCurrentClassLogger().Debug("RegisterEntityEvent AppPackage2.Start->" + result);
                Thread.Sleep(3 * 1000);
                NLog.LogManager.GetCurrentClassLogger().Debug("RegisterEntityEvent AppPackage2.End->" + result);
            });
        }
    }
}
