using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;

namespace HH.API.Services
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    public class RepositoryBase<T> where T : EntityBase
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
    }
}