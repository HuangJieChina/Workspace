using DapperExtensions;
using HH.API.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace HH.API.IServices
{
    /// <summary>
    /// 服务的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T> where T : EntityBase, new()
    {

        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        dynamic Insert(T t);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        bool Update(T t);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="t"></param>
        /// <param name="dbConnection"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        bool Update(T t, DbConnection dbConnection, DbTransaction dbTransaction);

        /// <summary>
        /// 获取单个实体对象
        /// </summary>
        /// <param name="objectId">实体对象Id</param>
        /// <returns>实体对象</returns>
        T GetObjectById(string objectId);

        /// <summary>
        /// 获取数据表记录总数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        bool RemoveObjectById(string objectId);

        /// <summary>
        /// 删除一个实体对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool RemoveObject(T t);

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">当前页码(从1开始)</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="sort">排序方式</param>
        /// <returns>返回当前页码结果集</returns>
        List<T> GetPageList(int pageIndex, int pageSize, out long recordCount,
            object predicate = null, IList<ISort> sort = null);

        /// <summary>
        /// 执行自定义SQL进行分页查询
        /// </summary>
        /// <param name="sqlCount">统计Sql</param>
        /// <param name="sqlQuery">查询结果集Sql</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="recordCount">返回结果集总数</param>
        /// <param name="predicate">动态查询条件</param>
        /// <param name="sort">排序方式(不可以空)</param>
        /// <returns>返回当前页码结果集</returns>
        List<dynamic> GetPageList(string sqlCount, string sqlQuery, int pageIndex,
            int pageSize, out long recordCount,
            object predicate = null, IList<ISort> sort = null);

        /// <summary>
        /// 清除缓存操作
        /// </summary>
        void ClearCache();

        /// <summary>
        /// 启动优先级比较高的进程
        /// </summary>
        void ElapsedHighThread();

        /// <summary>
        /// 启动优先级比较低的线程
        /// </summary>
        void ElapsedLowerThread();
    }
}
