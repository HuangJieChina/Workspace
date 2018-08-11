using HH.API.Entity;
using HH.API.Entity.Instance;
using HH.API.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HH.API.Services.Base
{
    public class EntityEventBus : IEntityEventBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="corpId"></param>
        public EntityEventBus(string corpId)
        {
            this.CorpId = corpId;
        }

        public string CorpId { get; }

        public Dictionary<Type, List<object>> AfterInsertEvents { get; } = new Dictionary<Type, List<object>>();
        public Dictionary<Type, List<object>> BeforeInsertEvents { get; } = new Dictionary<Type, List<object>>();

        public void RegisterAfterInsertEvent<T>(Action<T, dynamic> action) where T : EntityBase
        {
            if (!this.AfterInsertEvents.ContainsKey(typeof(T)))
            {
                this.AfterInsertEvents.Add(typeof(T), new List<object>());
            }
            this.AfterInsertEvents[typeof(T)].Add(action);
        }

        public void RegisterBeforeInsertEvent<T>(Func<T, T> func) where T : EntityBase
        {
            if (!this.BeforeInsertEvents.ContainsKey(typeof(T)))
            {
                this.BeforeInsertEvents.Add(typeof(T), new List<object>());
            }
            this.BeforeInsertEvents[typeof(T)].Add(func);
        }

        public void RegisterAfterRemoveEvent<T>(Action<string, dynamic> func) where T : EntityBase
        {

        }

        public void RegisterAfterUpdateEvent<T>(Action<T, dynamic> action) where T : EntityBase
        {
            throw new NotImplementedException();
        }


        public void RegisterBeforeRemoveEvent<T>(Func<string, bool> func) where T : EntityBase
        {
            throw new NotImplementedException();
        }

        public void RegisterBeforeUpdateEvent<T>(Func<T, T> func) where T : EntityBase
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 事件执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="result"></param>
        public void TriggerAfterInsertEvent<T>(T t, dynamic result) where T : EntityBase
        {// 未注册事件，则不执行
            if (!this.AfterInsertEvents.ContainsKey(typeof(T))) return;

            foreach (object obj in this.AfterInsertEvents[typeof(T)])
            {
                Task.Run(() =>
                {
                    Action<T, dynamic> action = obj as Action<T, dynamic>;
                    action(t, result);
                });
            }
        }

        /// <summary>
        /// 事件执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void TriggerBeforeInsertEvent<T>(T t) where T : EntityBase
        {
            if (!this.BeforeInsertEvents.ContainsKey(typeof(T))) return;

            foreach (object obj in this.AfterInsertEvents[typeof(T)])
            {
                Task.Run(() =>
                {
                    Action<T> action = obj as Action<T>;
                    action(t);
                });
            }
        }

        // End Class
    }
}
