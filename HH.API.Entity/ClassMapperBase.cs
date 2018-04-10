using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

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

        protected override void AutoMap(Func<Type, PropertyInfo, bool> canMap)
        {
            Type type = typeof(T);

            bool hasDefinedKey = Properties.Any(p => p.KeyType != KeyType.NotAKey);
            PropertyMap keyMap = null;
            foreach (var propertyInfo in type.GetProperties())
            {
                if (Properties.Any(p => p.Name.Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    continue;
                }

                Attribute notMapped = propertyInfo.GetCustomAttribute(typeof(NotMappedAttribute));
                if (notMapped != null)
                {
                    continue;
                }

                if ((canMap != null && !canMap(type, propertyInfo)))
                {
                    continue;
                }

                PropertyMap map = Map(propertyInfo);
                if (!hasDefinedKey)
                {
                    if (string.Equals(map.PropertyInfo.Name, "id", StringComparison.InvariantCultureIgnoreCase))
                    {
                        keyMap = map;
                    }

                    if (keyMap == null && map.PropertyInfo.Name.EndsWith("id", true, CultureInfo.InvariantCulture))
                    {
                        keyMap = map;
                    }
                }
            }

            if (keyMap != null)
            {
                keyMap.Key(PropertyTypeKeyTypeMapping.ContainsKey(keyMap.PropertyInfo.PropertyType)
                    ? PropertyTypeKeyTypeMapping[keyMap.PropertyInfo.PropertyType]
                    : KeyType.Assigned);
            }
        }


    }
}