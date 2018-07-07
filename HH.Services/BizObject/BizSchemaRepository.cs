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
            dynamic res = null;
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                res = conn.Insert<BizProperty>(property);
            }

            // 更新缓存
            if (res != null)
            {
                BizSchema bizSchema = this.GetObjectByKeyFromCache(BizSchema.PropertyName_SchemaCode, property.SchemaCode);
                if (bizSchema != null)
                {
                    bizSchema.Properties.Add(property);
                }
            }
            return res;
        }

        /// <summary>
        /// 获取数据模型
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <returns></returns>
        public BizSchema GetBizSchemaByCode(string schemaCode)
        {
            return this.GetObjectByKey(BizSchema.PropertyName_SchemaCode,
                schemaCode,
                (conn, bizSchema) =>
                 {
                     // 查询条件
                     IList<IPredicate> predList = new List<IPredicate>();
                     IFieldPredicate fieldPredicate = Predicates.Field<BizProperty>(p => p.SchemaCode, Operator.Eq, bizSchema.SchemaCode);
                     predList.Add(fieldPredicate);

                     IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

                     // SortOrder
                     List<ISort> sort = new List<ISort>();
                     sort.Add(new Sort() { Ascending = true, PropertyName = BizProperty.PropertyName_SortOrder });

                     bizSchema.Properties = conn.GetList<BizProperty>(predGroup, sort).ToList();
                 });
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override dynamic Insert(BizSchema t)
        {
            dynamic result;
            if (this.GetBizSchemaByCode(t.SchemaCode) != null)
            {
                throw new Exception(string.Format("Schema code {{0}} is exists", t.SchemaCode));
            }

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
            BizSchema bizSchema = this.GetBizSchemaByCode(schemaCode);
            if (bizSchema == null)
            {
                throw new Exception(string.Format("Schema code {{0}} is not exists", schemaCode));
            }

            // 创建表结构
            DbHandlerFactory.Instance.GetDefaultDbHandler<BizSchema>().RegisterBizTable(bizSchema);

            return true;
        }

        // End class
    }
}