using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Reflection;
using DapperExtensions.Sql;
using System.Data.Common;
using HH.API.IServices;
using System.Threading;
using HH.API.Common;
using HH.API.Entity.Database;
using HH.API.Entity.Cache.KeyCollectionCache;
using HH.API.Entity.Cache.EntityCache;

namespace HH.API.Services
{
    /// <summary>
    /// 数据访问基类
    /// TODO:待增加缓存机制
    /// TODO:待增加异步函数
    /// </summary>
    public partial class RepositoryBase<T> : IRepositoryBase<T>
        where T : EntityBase, new()
    {
        /// <summary>
        /// 基类构造函数
        /// </summary>
        public RepositoryBase()
        {
            DapperExtensions.DapperExtensions.Configure(typeof(ClassMapperBase<>),
                new List<Assembly>(),
                SqlDialect);

            // 表结构验证
            this.CheckTableSchema();

            // 注册码校验
            ServiceInit.Instance.Initial();
        }

        private ISqlDialect sqlDialect = null;
        /// <summary>
        /// 获取或设置SQL构造对象
        /// </summary>
        protected ISqlDialect SqlDialect
        {
            get
            {
                if (this.sqlDialect == null)
                {
                    switch (ConnectionFactory.DatabaseType)
                    {
                        case DatabaseType.MySql:
                            this.sqlDialect = new MySqlDialect();
                            break;
                        case DatabaseType.SqlServer:
                            this.sqlDialect = new SqlServerDialect();
                            break;
                        default:
                            throw new Exception("此数据库类型未实现!");
                    }
                }
                return this.sqlDialect;
            }
        }

        #region 缓存对象 --------------------------
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
                    this._EntityCache = EntityCacheFactory<T>.Instance.GetCache(this.TableName);
                }
                return this._EntityCache;
            }
        }

        private Dictionary<string, IKeyCache<T>> _KeyCache = null;
        /// <summary>
        /// 获取实体(Key)缓存类
        /// </summary>
        public Dictionary<string, IKeyCache<T>> KeyCache
        {
            get
            {
                if (this._KeyCache == null)
                {
                    this._KeyCache = new Dictionary<string, IKeyCache<T>>();
                }
                return this._KeyCache;
            }
        }
        #endregion

        private IEntityEventBus _EventBus = null;

        /// <summary>
        /// 获取事件注册对象
        /// </summary>
        public IEntityEventBus EventBus
        {
            get
            {
                if (this._EventBus == null)
                {
                    this._EventBus = ServiceFactory.Instance.GetRepository<IEntityEventBus>();
                }
                return this._EventBus;
            }
        }

        /// <summary>
        /// 获取当前实例对应的CorpId
        /// </summary>
        // public string CorpId { get; set; }

        /// <summary>
        /// 获取Key缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private IKeyCache<T> GetKeyCache(string key)
        {
            if (this.KeyCache.ContainsKey(key)) return this.KeyCache[key];

            try
            {
                Monitor.Enter(this);
                if (!this.KeyCache.ContainsKey(key))
                {
                    IKeyCache<T> currentKeyCache = KeyCacheFactory<T>.Instance.GetCache(this.TableName);
                    this.KeyCache.Add(key, currentKeyCache);
                }
                return this.KeyCache[key];
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        /// <summary>
        /// 确认表结构已经创建
        /// </summary>
        private void CheckTableSchema()
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                conn.Get<T>(Guid.Empty.ToString());
            }
        }

        /// <summary>
        /// 获取数据库连接对象(注意使用后一定要立即关闭)
        /// </summary>
        /// <returns></returns>
        public DbConnection OpenConnection()
        {
            return ConnectionFactory.DefaultConnection();
        }

        #region Lock Properties ---------------------------
        private KeyLock keyLock = null;
        /// <summary>
        /// 获取KeyLock
        /// </summary>
        public KeyLock KeyLock
        {
            get
            {
                if (this.keyLock == null)
                {
                    this.keyLock = new KeyLock();
                }
                return this.keyLock;
            }
        }
        #endregion

        #region 数据库读写操作  ---------------------------
        /// <summary>
        /// 批量执行SQL语句
        /// </summary>
        /// <param name="commandContexts"></param>
        public virtual int ExecuteSql(List<CommandDefinition> commandDefinitions)
        {
            return TransactionFunc<int>((conn, transaction) =>
             {
                 int count = 0;
                 foreach (CommandDefinition commandDefinition in commandDefinitions)
                 {
                     count += conn.Execute(commandDefinition.CommandText,
                         commandDefinition.Parameters,
                         transaction,
                         commandDefinition.CommandTimeout,
                         commandDefinition.CommandType);
                 }
                 return count;
             });
        }

        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        public virtual dynamic Insert(T t)
        {
            this.EventBus.TriggerBeforeInsertEvent<T>(t);

            dynamic result = TransactionFunc<dynamic>(conn =>
            {
                dynamic res = conn.Insert<T>(t);
                this.EntityCache.Save(t);
                return res;
            });

            this.EventBus.TriggerAfterInsertEvent<T>(t, result);
            return result;
        }

        #region 带事务的方式执行SQL ----------------------
        /// <summary>
        /// 执行带事务的操作
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public virtual dynamic TransactionFunc<TResult>(Func<DbConnection, TResult> func)
        {
            TResult result = default(TResult);

            using (var conn = ConnectionFactory.DefaultConnection())
            {
                DbTransaction transaction = null;
                try
                {
                    transaction = conn.BeginTransaction();

                    result = func(conn);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                }
            }
            return result;
        }

        public virtual dynamic TransactionFunc<TResult>(Func<DbConnection, DbTransaction, TResult> func)
        {
            TResult result = default(TResult);

            using (var conn = ConnectionFactory.DefaultConnection())
            {
                DbTransaction transaction = null;
                try
                {
                    transaction = conn.BeginTransaction();

                    result = func(conn, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        public virtual bool Update(T t)
        {
            return TransactionFunc<bool>(conn =>
            {
                // 先更新数据库，再更新缓存
                bool res = conn.Update<T>(t);
                if (res) this.EntityCache.Save(t);
                return res;
            });
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="t"></param>
        /// <param name="dbConnection"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public virtual bool Update(T t, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            bool res = dbConnection.Update<T>(t, dbTransaction);
            if (res) this.EntityCache.Save(t);
            return res;
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
        /// 从缓存中读取
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual T GetObjectByKeyFromCache(string key, string value)
        {
            IKeyCache<T> currentCache = this.GetKeyCache(key);
            return currentCache.ContainsKey(value) ? currentCache.Get(value) : null;
        }

        /// <summary>
        /// 加入缓存
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void SaveObjectByKeyToCache(T obj, string key, string value)
        {
            // KeyCache和EntityCache指向同一个实例
            IKeyCache<T> currentCache = this.GetKeyCache(key);
            if (this.EntityCache.Get(obj.ObjectId) != null)
            {
                currentCache.Save(value, this.EntityCache.Get(obj.ObjectId));
            }
            else
            {
                this.EntityCache.Save(obj); // 注意：这里不是完全线程安全的
                // 加入缓存
                currentCache.Save(value, obj);
            }
        }

        /// <summary>
        /// 从缓存中获取
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual T GetObjectByKey(string key, string value)
        {
            return this.GetObjectByKey(key, value, null);
        }

        /// <summary>
        /// 获取单个实体对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual T GetObjectByKey(string key, string value, Action<DbConnection, T> action)
        {
            T result = this.GetObjectByKeyFromCache(key, value);
            if (result != default(T)) return result;

            string sql = string.Format("SELECT * FROM {0} WHERE {1}={2}{1}", this.TableName, key,
                this.SqlDialect.ParameterPrefix);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(key, value);

            using (var conn = ConnectionFactory.DefaultConnection())
            {
                result = conn.QueryFirstOrDefault<T>(sql, parameters);
                if (action != null && result != null) action(conn, result);
            }

            // 获取到的是空值
            if (result != null)
            {
                this.SaveObjectByKeyToCache(result, key, value);
            }
            return result;
        }

        /// <summary>
        /// 获取实体对象集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual List<T> GetListByKey(string key, string value)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE {1}={2}{1}", this.TableName, key,
                this.SqlDialect.ParameterPrefix);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(key, value);

            List<T> result = default(List<T>);
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                result = conn.Query<T>(sql, parameters).AsList();
            }
            return result;
        }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual List<T> GetList(object predicate, IList<ISort> sort = null)
        {
            if (predicate == null)
            {
                throw new Exception("predicate不允许为空，或者使用GetAll方法!");
            }

            using (DbConnection conn = this.OpenConnection())
            {
                return conn.GetList<T>(predicate, sort).AsList();
            }
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T GetSingle(object predicate = null)
        {
            List<T> result = this.GetList(predicate);
            if (result != null && result.Count > 0) return result[0];
            return default(T);
        }

        /// <summary>
        /// 获取数据表记录总数
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                return conn.Count<T>(null);
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

        /// <summary>
        /// 删除一个实体对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool RemoveObject(T t)
        {
            return TransactionFunc<bool>((conn) =>
            {
                bool res = conn.Delete<T>(t);
                if (res) this.EntityCache.Remove(t.ObjectId);
                return res;
            });
        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetAll()
        {
            IList<ISort> sort = new List<ISort>();
            sort.Add(new Sort() { Ascending = false, PropertyName = EntityBase.PropertyName_CreatedTime });
            return this.GetAll(sort);
        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public virtual List<T> GetAll(IList<ISort> sort = null)
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                return conn.GetList<T>(null, sort).AsList<T>();
            }
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">当前页码(从1开始)</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="sort">排序方式</param>
        /// <returns>返回当前页码结果集</returns>
        public virtual List<T> GetPageList(int pageIndex, int pageSize, out long recordCount,
            object predicate = null, IList<ISort> sort = null)
        {
            List<T> res = null;
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                // 默认按照事件倒序排序
                if (sort == null)
                {
                    sort = new List<ISort>();
                    sort.Add(new Sort() { Ascending = false, PropertyName = EntityBase.PropertyName_CreatedTime });
                }
                conn.GetPage<T>(predicate, sort, pageIndex - 1, pageSize).AsList();

                if ((res == null || res.Count == 0) && pageIndex == 1)
                {// 列表集合为空，不做总数查询
                    recordCount = 0;
                }
                else
                {
                    recordCount = conn.Count<T>(predicate);
                }
            }
            return res;
        }

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
        public virtual List<dynamic> GetPageList(string sqlCount, string sqlQuery, int pageIndex,
            int pageSize, out long recordCount,
            object predicate = null, IList<ISort> sort = null)
        {
            List<dynamic> res;

            using (DbConnection conn = this.OpenConnection())
            {
                res = conn.GetPage<dynamic>(sqlQuery, predicate, sort, pageIndex - 1, pageSize).AsList<dynamic>();
                if ((res == null || res.Count == 0) && pageIndex == 1)
                {// 列表集合为空，不做总数查询
                    recordCount = 0;
                }
                else
                {
                    recordCount = conn.Count(sqlCount, predicate);
                }
            }

            return res;
        }
        #endregion

        /// <summary>
        /// 清除当前缓存对象所有数据
        /// </summary>
        public virtual void ClearCache()
        {
            this.EntityCache.Clear();
            this.KeyCache.Clear();
        }

        public virtual void ElapsedHighThread()
        {

        }

        public virtual void ElapsedLowerThread()
        {

        }

        /// <summary>
        /// 获取查询化参数的前缀词
        /// </summary>
        public char ParameterPrefix
        {
            get
            {
                return this.SqlDialect.ParameterPrefix;
            }
        }

        /// <summary>
        /// 获取数据存储表名称
        /// </summary>
        public string TableName
        {
            get
            {
                return new T().TableName;
            }
        }

        // End class
    }
}