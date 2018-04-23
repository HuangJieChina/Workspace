using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;

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
                return conn.Insert<T>(t);
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
                return conn.Update<T>(t);
            }
        }

        /// <summary>
        /// 获取单个实体对象
        /// </summary>
        /// <param name="objectId">实体对象Id</param>
        /// <returns>实体对象</returns>
        public virtual T GetObjectById(string objectId)
        {
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
            return this.RemoveObject(new T() { ObjectId = objectId });
        }

        public virtual bool RemoveObject(T t)
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                return conn.Delete<T>(t);
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


    }
}