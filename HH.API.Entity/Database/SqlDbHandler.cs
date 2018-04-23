using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace HH.API.Entity.Database
{
    /// <summary>
    /// SQL数据库的操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlDbHandler<T> : IDbHandler where T : EntityBase
    {
        public bool CreateTable(string tableName)
        {
            // 构造创建表的 SQL 语句
            StringBuilder sql = new StringBuilder();
            sql.Append("Create Table [" + tableName + "]");
            sql.Append("(");

            sql.Append(string.Format("[{0}] {1}", EntityBase.PropertyName_ObjectId, "char(36) Primary Key,"));
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Attribute notMapped = property.GetCustomAttribute(typeof(NotMappedAttribute));
                if (notMapped != null)
                {
                    continue;
                }
                if (property.Name.Equals(EntityBase.PropertyName_ObjectId, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                DbColumn column = this.GetColumnFromPropertyInfo(property);
                sql.Append(column.ToString());
                sql.Append(",");
            }

            sql.Remove(sql.Length - 1, 1); // 移除最后一个 ，符号
            sql.Append(")");
            return ConnectionFactory.ExecuteNonQuery(sql.ToString()) > 0;
        }

        /// <summary>
        /// 判断数据表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool IsExistsTable(string tableName)
        {
            // TODO:代码待优化
            string sql = string.Format("SELECT 1 FROM sysobjects where xtype='U' AND name='{0}'", tableName);
            return ConnectionFactory.ExecuteScalar(sql) != null;
        }

        /// <summary>
        /// 修改数据表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool ModifyTable(string tableName)
        {
            string sqlColumn = string.Format(@"SELECT a.name as columnname,c.name as typename,a.length FROM 
                                                    syscolumns a 
                                                    inner JOIN sysobjects b ON a.id=b.id 
                                                    Inner JOIN systypes c ON a.xtype=c.xtype
                                                    where b.XType='U'
                                                    And b.Name='{0}'
                                                    And c.status=0", tableName);

            PropertyInfo[] properties = typeof(T).GetProperties();

            Dictionary<string, DbColumn> columns = new Dictionary<string, DbColumn>();

            ConnectionFactory.ExecuteSqlReader(sqlColumn, (reader) =>
            {
                while (reader.Read())
                {
                    if (!reader.HasRows)
                    {
                        throw new Exception("table not exists,tableName=" + tableName);
                    }
                    string columnName = reader.GetFieldValue<string>(0);
                    string typeName = reader.GetFieldValue<string>(1);

                    int length = int.Parse(reader.GetValue(2).ToString());
                    columns.Add(columnName.ToLower(), new DbColumn(columnName, typeName, length));
                }
            });

            // 注：这里不删除业务字段
            foreach (PropertyInfo property in properties)
            {
                // 主键不做变化
                if (property.Name == EntityBase.PropertyName_ObjectId) continue;

                DbColumn column = null;
                column = this.GetColumnFromPropertyInfo(property);

                string alertSql = string.Empty;
                if (!columns.ContainsKey(property.Name.ToLower()))
                {// 新增字段
                    columns.Remove(property.Name.ToLower());
                    alertSql = string.Format("alter table {0} add {1}", tableName, column.ToString());
                }
                else
                {// 修改字段
                    DbColumn oldColumn = columns[property.Name.ToLower()];
                    if (!oldColumn.CompareChange(property.Name, column.ColumnLength))
                    {
                        columns.Remove(property.Name.ToLower());
                        continue;
                    }
                    // 比较字段长度是否变化
                    alertSql = string.Format("alter table {0} alter column {1}", tableName, column.ToString());

                    columns.Remove(property.Name.ToLower());
                }

                // 处理字段的变化(新增和修改)
                ConnectionFactory.ExecuteNonQuery(alertSql);
            }

            // 处理被移除的字段
            foreach (string key in columns.Keys)
            {
                if (key.Equals(EntityBase.PropertyName_ObjectId, StringComparison.OrdinalIgnoreCase)) continue;
                string deleteSql = string.Format("alter table {0} drop column {1}", tableName, key);
                ConnectionFactory.ExecuteNonQuery(deleteSql);
            }
            return true;
        }

        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public DbColumn GetColumnFromPropertyInfo(PropertyInfo property)
        {
            DbColumn column = new DbColumn()
            {
                ColumnName = property.Name
            };

            // 获取字段长度
            StringLengthAttribute stringLengthAttribute = property.GetCustomAttribute(typeof(StringLengthAttribute)) as StringLengthAttribute;
            if (stringLengthAttribute != null)
            {
                column.ColumnLength = stringLengthAttribute.MaximumLength;
            }
            // 获取字段类型
            ColumnAttribute columnAttribute = property.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
            if (columnAttribute != null)
            {
                column.ColumnType = columnAttribute.TypeName;
            }
            else
            {
                if (property.PropertyType == typeof(string))
                {
                    column.ColumnType = "nvarchar";
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    column.ColumnType = "datetime";
                }
                else if (property.PropertyType == typeof(int))
                {
                    column.ColumnType = "int";
                }
                else if (property.PropertyType == typeof(decimal))
                {
                    column.ColumnType = "decimal";
                }
                else if (property.PropertyType == typeof(float))
                {
                    column.ColumnType = "float";
                }
                else
                {
                    column.ColumnType = "tinyint";
                }
            }
            return column;
        }
    }

    /// <summary>
    /// 数据库字段
    /// </summary>
    public class DbColumn
    {
        public DbColumn() { }

        public DbColumn(string columnName, string columnType, int columnLength)
        {
            this.ColumnName = columnName;
            this.ColumnType = columnType;
            this.ColumnLength = columnLength;
        }

        /// <summary>
        /// 获取或设置是否是主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 获取或设置字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 获取或设置字段类型
        /// </summary>
        public string ColumnType { get; set; }

        public const int DefaultColumnLength = 50;

        private int _ColumnLength = DefaultColumnLength;

        /// <summary>
        /// 获取或设置字段长度
        /// </summary>
        public int ColumnLength
        {
            get { return this._ColumnLength; }
            set { this._ColumnLength = value; }
        }

        /// <summary>
        /// 比较是否发生变化
        /// </summary>
        /// <param name="columnType"></param>
        /// <param name="columnLenth"></param>
        /// <returns></returns>
        public bool CompareChange(string columnType, int columnLenth)
        {
            // 非字符类型，不做长度变更
            if (!columnType.Contains("char")) return false;

            return ColumnLength != columnLenth;
        }

        /// <summary>
        /// 转换为String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.ColumnType.Contains("char"))
            {
                return string.Format("{0} {1}({2})", this.ColumnName, this.ColumnType, this.ColumnLength);
            }
            else if (this.ColumnType.Equals("decimal"))
            {
                return string.Format("{0} {1}(18,4)", this.ColumnName, this.ColumnType);
            }
            else
            {
                return string.Format("{0} {1}", this.ColumnName, this.ColumnType);
            }
        }
    }
}
