using HH.API.Entity.App;
using HH.API.Entity.Orgainzation;
using HH.API.IServices;
using HH.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HH.API.Events
{
    public class RegisterEntityEvent
    {
        public RegisterEntityEvent()
        {
            this.entityEventBus = ServiceFactory.Instance.GetRepository<IEntityEventBus>();
        }

        public IEntityEventBus entityEventBus = null;

        public void RegisterUserEnvent()
        {
            this.entityEventBus.RegisterAfterInsertEvent<OrgUser>((user, result) =>
            {
                NLog.LogManager.GetCurrentClassLogger().Debug("调试信息 RegisterEntityEvent->" + result);
            });

            this.entityEventBus.RegisterAfterInsertEvent<AppPackage>((package, result) =>
            {
                NLog.LogManager.GetCurrentClassLogger().Debug("RegisterEntityEvent AppPackage1.Start->" + result);
                Thread.Sleep(3 * 1000);
                NLog.LogManager.GetCurrentClassLogger().Debug("RegisterEntityEvent AppPackage1.End->" + result);
            });

            this.entityEventBus.RegisterAfterInsertEvent<AppPackage>((package, result) =>
            {
                NLog.LogManager.GetCurrentClassLogger().Debug("RegisterEntityEvent AppPackage2.Start->" + result);
                Thread.Sleep(3 * 1000);
                NLog.LogManager.GetCurrentClassLogger().Debug("RegisterEntityEvent AppPackage2.End->" + result);
            });
        }
    }
}