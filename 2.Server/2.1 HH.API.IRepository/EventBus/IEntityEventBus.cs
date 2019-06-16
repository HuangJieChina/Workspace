using HH.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.IRepository
{
    /// <summary>
    /// 实体类事件注册接口
    /// </summary>
    public interface IEntityEventBus
    {
        /// <summary>
        /// 注册新增后事件
        /// </summary>
        /// <param name="action"></param>
        void RegisterAfterInsertEvent<T>(Action<T, dynamic> action) where T : EntityBase;

        /// <summary>
        /// 注册新增前事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        void RegisterBeforeInsertEvent<T>(Func<T, T> func) where T : EntityBase;

        /// <summary>
        /// 注册更新后事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        void RegisterAfterUpdateEvent<T>(Action<T, dynamic> action) where T : EntityBase;

        /// <summary>
        /// 注册更新前事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        void RegisterBeforeUpdateEvent<T>(Func<T, T> func) where T : EntityBase;

        /// <summary>
        /// 注册删除前事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        void RegisterBeforeRemoveEvent<T>(Func<string, bool> func) where T : EntityBase;

        /// <summary>
        /// 注册删除后事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        void RegisterAfterRemoveEvent<T>(Action<string, dynamic> func) where T : EntityBase;

        void TriggerAfterInsertEvent<T>(T t, dynamic result) where T : EntityBase;

        void TriggerBeforeInsertEvent<T>(T t) where T : EntityBase;
    }
}