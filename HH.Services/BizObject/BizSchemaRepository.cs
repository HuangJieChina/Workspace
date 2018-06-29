using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.Entity.BizObject;
using HH.API.IServices;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;

namespace HH.API.Services
{
    public class BizSchemaRepository : RepositoryBase<BizSchema>, IBizSchemaRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BizSchemaRepository()
        {
            
        }

        public dynamic AddBizProperty(BizProperty property)
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                return conn.Insert<BizProperty>(property);
            }
        }

        public BizSchema GetBizSchemaByCode(string schemaCode)
        {
            return this.GetObjectByKey(BizSchema.PropertyName_SchemaCode, schemaCode);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override dynamic Insert(BizSchema t)
        {
            dynamic result;
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                result = conn.Insert<BizSchema>(t);
                conn.Insert<BizProperty>(t.Properties);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <returns></returns>
        public bool PublishBizSchema(string schemaCode)
        {
            dynamic obj = new System.Dynamic.ExpandoObject();

            // 创建表结构
            return true;
        }

        // End class
    }
}