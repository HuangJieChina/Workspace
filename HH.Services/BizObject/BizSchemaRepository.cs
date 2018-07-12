using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IServices;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using HH.API.Entity.BizModel;

namespace HH.API.Services
{
    /// <summary>
    /// 业务模型数据存储
    /// </summary>
    public class BizSchemaRepository : RepositoryBase<BizSchema>, IBizSchemaRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BizSchemaRepository()
        {

        }

        /// <summary>
        /// 新增业务属性
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public dynamic AddBizProperty(BizProperty property)
        {
            //if (string.IsNullOrWhiteSpace(property.SchemaCode))
            //{
            //    throw new Exception("Schema code can not be empty.");
            //}

            try
            {
                this.KeyLock.Lock(property.SchemaCode);
                BizSchema bizSchema = this.GetBizSchemaByCode(property.SchemaCode);
                //if (bizSchema == null)
                //{// 业务模型不存在
                //    throw new Exception(string.Format("Schema is not exists,which schema code is {0}.", property.SchemaCode));
                //}

                //if (bizSchema.Properties.Exists((p) => p.PropertyCode == property.PropertyCode))
                //{// 相同编码已经存在
                //    throw new Exception(string.Format("This property code {0} is alerady exists.", property.PropertyCode));
                //}

                dynamic res = null;
                using (var conn = ConnectionFactory.DefaultConnection())
                {
                    res = conn.Insert<BizProperty>(property);
                }

                // 更新缓存
                if (res != null)
                {
                    bizSchema.Properties.Add(property);
                }
                return res;
            }
            finally
            {
                this.KeyLock.UnLock(property.SchemaCode);
            }
        }

        
        public BizProperty GetBizProperty(string objectId)
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                return conn.Get<BizProperty>(objectId);
            }
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
            //if (this.GetBizSchemaByCode(t.SchemaCode) != null)
            //{
            //    throw new Exception(string.Format("Schema code {{0}} is exists", t.SchemaCode));
            //}

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
            //if (bizSchema == null)
            //{
            //    throw new Exception(string.Format("Schema code {{0}} is not exists", schemaCode));
            //}

            // 创建表结构
            DbHandlerFactory.Instance.GetDefaultDbHandler<BizSchema>().RegisterBizTable(bizSchema);

            return true;
        }

        // End class
    }
}