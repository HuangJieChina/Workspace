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
using HH.API.Entity.BizData;
using HH.API.Entity.BizModel;
using HH.API.Entity.Database;

namespace HH.API.Services
{
    /// <summary>
    /// 业务对象的存储
    /// </summary>
    public class BizObjectRepository : BizSchemaRepository, IBizObjectRepository
    {
        public BizObjectRepository(string corpId) : base(corpId)
        {
        }

        /// <summary>
        /// 新增业务对象
        /// </summary>
        /// <param name="bizObject"></param>
        /// <returns></returns>
        public dynamic AddBizObject(BizObject bizObject)
        {
            BizSchema bizSchema = bizObject.Schema;
            if (bizSchema == null)
            {
                bizSchema = this.GetBizSchemaByCode(bizObject.SchemaCode);
            }
            bizObject.CreatedTime = DateTime.Now;

            List<CommandDefinition> commandDefinitions = this.GeneralInsert(bizSchema, bizObject);
            return this.ExecuteSql(commandDefinitions) > 0 ? bizObject.ObjectId : string.Empty;
        }

        public BizObject GetBizObjectById(string schemaCode, string objectId)
        {
            BizSchema schema = this.GetBizSchemaByCode(schemaCode);

            if (schema == null) return null;

            BizObject bizObject = null;
            dynamic result;
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                result = conn.QueryFirstOrDefault(string.Format("SELECT * FROM {0}",
                    schema.DataTableName));
            };
            // TODO:处理数据格式和子表
            foreach (BizProperty property in schema.Properties)
            {
                bizObject.ValueTables[property.PropertyCode] = ((IDictionary<string, object>)result)[property.PropertyCode] + string.Empty;
            }
            return bizObject;
        }

        /// <summary>
        /// 查询业务对象(不包含子表)
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<BizObject> GetBizObjects(string schemaCode,
            object predicate = null,
            IList<ISort> sort = null)
        {
            throw new NotImplementedException();
        }

        public bool RemoveBizObject(string schemaCode, string objectId)
        {
            BizSchema schema = this.GetBizSchemaByCode(schemaCode);

            if (schema == null) return false;
            string sql = string.Format("DELETE * FROM {0} Where {1}='{2}'",
                    schema.DataTableName,
                    BizSchema.PropertyName_ObjectId,
                    objectId);

            // TODO:移除子表相关内容
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                int res = conn.Execute(sql);
                return res > 0;
            };
        }

        /// <summary>
        /// 更新业务对象
        /// </summary>
        /// <param name="bizObject">业务对象</param>
        /// <returns></returns>
        public bool UpdateBizObject(BizObject bizObject)
        {
            BizSchema bizSchema = bizObject.Schema;
            if (bizSchema == null)
            {
                bizSchema = this.GetBizSchemaByCode(bizObject.SchemaCode);
            }
            List<CommandDefinition> commandDefinitions = this.GeneralUpdate(bizSchema, bizObject);
            return this.ExecuteSql(commandDefinitions) > 0;
        }

        /// <summary>
        /// 生成 Insert 语句
        /// </summary>
        /// <param name="bizSchema"></param>
        /// <returns></returns>
        private List<CommandDefinition> GeneralUpdate(BizSchema bizSchema, BizObject bizObject)
        {
            List<CommandDefinition> commandDefinitions = new List<CommandDefinition>();
            this.GeneralUpdate(commandDefinitions, bizSchema.DataTableName, bizSchema.Properties, bizObject);

            return commandDefinitions;
        }

        /// <summary>
        /// 递归获取 Insert 的语句
        /// </summary>
        /// <param name="commandDefinitions"></param>
        /// <param name="tableName"></param>
        /// <param name="properties"></param>
        /// <param name="bizObject"></param>
        private void GeneralUpdate(List<CommandDefinition> commandDefinitions, string tableName,
            List<BizProperty> properties, BizObject bizObject)
        {
            var columns = properties.FindAll((p) => p.IsRealyColumn);

            string sql = this.GeneralUpsertSql(tableName, columns);

            // 构造 Sql 语句
            DynamicParameters parameters = new DynamicParameters();
            foreach (var column in columns)
            {
                parameters.Add(column.PropertyCode, bizObject.GetValue(column.PropertyCode));
            }
            commandDefinitions.Add(new CommandDefinition(sql, parameters));

            // 递归子表对象
            List<BizProperty> bizProperties = properties.FindAll((p) => p.LogicType == LogicType.SubTable);
            if (bizProperties != null && bizProperties.Count > 0)
            {
                foreach (BizProperty bizProperty in bizProperties)
                {
                    List<BizObject> subObjects = bizObject.GetValue<List<BizObject>>(bizProperty.PropertyCode);
                    if (subObjects != null)
                    {
                        foreach (BizObject subObject in subObjects)
                        {
                            this.GeneralInsert(commandDefinitions, tableName, bizProperty.Properties, subObject);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取Upsert的Sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private string GeneralUpsertSql(string tableName,
            List<BizProperty> columns)
        {
            return string.Format("if exists(select 1 from {0} where {1}={2}{1}) {3} else {4}",
                  tableName,
                  BizSchema.PropertyName_ObjectId,
                  this.ParameterPrefix,
                  this.GeneralUpdateSql(tableName, columns),
                  this.GeneralInsertSql(tableName, columns));
        }

        private string GeneralInsertSql(string tableName, List<BizProperty> columns)
        {
            var columnNames = columns.Select(p => p.PropertyCode);
            var values = columns.Select(p => this.ParameterPrefix + p.PropertyCode);

            return string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                                       tableName,
                                       columnNames.AppendStrings(),
                                       values.AppendStrings());
        }

        /// <summary>
        /// 生成更新的Sql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private string GeneralUpdateSql(string tableName,
             List<BizProperty> columns)
        {
            var setSql =
                 columns.Select(
                     p =>
                     string.Format(
                         "{0} = {1}{0}", p.PropertyCode, this.ParameterPrefix));

            return string.Format("UPDATE {0} SET {1} WHERE {2}={3}{2}",
                tableName,
                setSql.AppendStrings(),
                BizSchema.PropertyName_ObjectId,
                this.ParameterPrefix);
        }

        #region 构造Insert语句 -------------------
        /// <summary>
        /// 生成 Insert 语句
        /// </summary>
        /// <param name="bizSchema"></param>
        /// <returns></returns>
        private List<CommandDefinition> GeneralInsert(BizSchema bizSchema, BizObject bizObject)
        {
            List<CommandDefinition> commandDefinitions = new List<CommandDefinition>();
            this.GeneralInsert(commandDefinitions, bizSchema.DataTableName, bizSchema.Properties, bizObject);

            return commandDefinitions;
        }

        /// <summary>
        /// 递归获取 Insert 的语句
        /// </summary>
        /// <param name="commandDefinitions"></param>
        /// <param name="tableName"></param>
        /// <param name="properties"></param>
        /// <param name="bizObject"></param>
        private void GeneralInsert(List<CommandDefinition> commandDefinitions, string tableName,
            List<BizProperty> properties, BizObject bizObject)
        {
            var columns = properties.FindAll((p) => p.IsRealyColumn);

            string sql = this.GeneralInsertSql(tableName, columns);

            // 构造 Sql 语句
            DynamicParameters parameters = new DynamicParameters();
            foreach (var column in columns)
            {
                parameters.Add(column.PropertyCode, bizObject.GetValue(column.PropertyCode));
            }
            commandDefinitions.Add(new CommandDefinition(sql, parameters));

            // 递归子表对象
            List<BizProperty> bizProperties = properties.FindAll((p) => p.LogicType == LogicType.SubTable);
            if (bizProperties != null && bizProperties.Count > 0)
            {
                foreach (BizProperty bizProperty in bizProperties)
                {
                    List<BizObject> subObjects = bizObject.GetValue<List<BizObject>>(bizProperty.PropertyCode);
                    if (subObjects != null)
                    {
                        foreach (BizObject subObject in subObjects)
                        {
                            this.GeneralInsert(commandDefinitions, tableName, bizProperty.Properties, subObject);
                        }
                    }
                }
            }
        }
        #endregion

        // End class
    }
}