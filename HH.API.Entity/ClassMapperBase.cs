using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HH.API.Entity
{
    /// <summary>
    /// Dapper类和数据表映射关系基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClassMapperBase<T> : ClassMapper<T>
        where T : EntityBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        public ClassMapperBase()
        {
            // base.Table(T.TableName);
            Map(p => p.TableName).Ignore();
            Map(f => f.ObjectId).Key(KeyType.Identity);
            AutoMap();

            // TODO:创建数据库表结构
        }


    }
}