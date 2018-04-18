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

                // 判断是否为 Key 值
                // bool isKey = property.GetCustomAttribute(typeof(KeyAttribute)) != null;
                string typeName = string.Empty;
                int length = 50;
                StringLengthAttribute stringLengthAttribute = property.GetCustomAttribute(typeof(StringLengthAttribute)) as StringLengthAttribute;
                if (stringLengthAttribute != null)
                {
                    length = stringLengthAttribute.MaximumLength;
                }
                ColumnAttribute columnAttribute = property.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
                if (columnAttribute != null)
                {
                    typeName = columnAttribute.TypeName;
                    typeName += "(" + length.ToString() + ")";
                }
                else
                {
                    if (property.PropertyType == typeof(string))
                    {
                        typeName = "nvarchar(" + length.ToString() + ")";
                    }
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        typeName = "datetime";
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        typeName = "int";
                    }
                    else if (property.PropertyType == typeof(decimal))
                    {
                        typeName = "decimal(18,4)";
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        typeName = "float";
                    }
                    else
                    {
                        typeName = "tinyint";
                    }
                }
                sql.Append(string.Format("[{0}] {1}", property.Name, typeName));
                sql.Append(",");
            }
            sql.Remove(sql.Length - 1, 1); // 移除最后一个 ，符号
            // sql.Append()
            sql.Append(")");
            return ConnectionFactory.ExecuteNonQuery(sql.ToString()) > 0;
        }

        // public bool CreateTable(string tableName,)

        public bool IsExistsTable(string tableName)
        {
            // TODO:代码待优化
            string sql = string.Format("SELECT 1 FROM sysobjects where xtype='U' AND name='{0}'", tableName);
            return ConnectionFactory.ExecuteScalar(sql) != null;
        }

        public bool ModifyTable(string tableName)
        {
            return true;
        }
    }
}
