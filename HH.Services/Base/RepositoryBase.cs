using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Reflection;
using DapperExtensions.Sql;

namespace HH.API.Services
{
    /// <summary>
    /// 数据访问基类
    /// TODO:待增加缓存机制
    /// TODO:待增加异步函数
    /// </summary>
    public class RepositoryBase<T> where T : EntityBase, new()
    {
        /// <summary>
        /// 基类构造函数
        /// </summary>
        public RepositoryBase()
        {
            // 重新设置默认配置项
            DapperExtensions.DapperExtensions.Configure(typeof(ClassMapperBase<>),
                new List<Assembly>(),
                new SqlServerDialect());
        }

        private IEntityCache<T> _EntityCache = null;
        /// <summary>
        /// 获取实体缓存类
        /// </summary>
        public IEntityCache<T> EntityCache
        {
            get
            {
                if (this._EntityCache == null)
                {
                    this._EntityCache = HH.API.Entity.EntityCache.Factory<T>.Instance.GetCache();
                }
                return this._EntityCache;
            }
        }

        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        public virtual dynamic Insert(T t)
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                dynamic res = conn.Insert<T>(t);
                this.EntityCache.Save(t);
                return res;
            }
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        public virtual bool Update(T t)
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                bool res = conn.Update<T>(t);
                if (res) this.EntityCache.Save(t);
                return res;
            }
        }

        /// <summary>
        /// 获取单个实体对象
        /// </summary>
        /// <param name="objectId">实体对象Id</param>
        /// <returns>实体对象</returns>
        public virtual T GetObjectById(string objectId)
        {
            T t = this.EntityCache.Get(objectId);
            if (t != default(T))
            {
                return this.EntityCache.Get(objectId);
            }

            using (var conn = ConnectionFactory.DefaultConnection())
            {
                return conn.Get<T>(objectId);
            }
        }

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public virtual bool RemoveObjectById(string objectId)
        {
            bool res = this.RemoveObject(new T() { ObjectId = objectId });
            if (res) this.EntityCache.Remove(objectId);
            return res;
        }

        /// <summary>
        /// 删除一个实体对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool RemoveObject(T t)
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                bool res = conn.Delete<T>(t);
                if (res) this.EntityCache.Remove(t.ObjectId);
                return res;
            }
        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetAll()
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                return conn.GetList<T>().AsList<T>();
            }
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">当前页码(从1开始)</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="sort">排序值</param>
        /// <returns></returns>
        public virtual List<T> GetPageList(int pageIndex, int pageSize, out long recordCount,
            object predicate = null, IList<ISort> sort = null)
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                IDbCommand cmd = conn.CreateCommand();

                recordCount = conn.Count<T>(predicate);
                // 默认按照事件倒序排序
                if (sort == null)
                {
                    sort = new List<ISort>();
                    sort.Add(new Sort() { Ascending = false, PropertyName = EntityBase.PropertyName_CreatedTime });
                }
                return conn.GetPage<T>(predicate, sort, pageIndex - 1, pageSize).AsList();
            }
        }

    }
}