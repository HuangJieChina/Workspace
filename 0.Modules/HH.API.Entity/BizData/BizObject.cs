using HH.API.Entity.BizModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Entity.BizData
{
    /// <summary>
    /// 所有实体类的基类
    /// </summary>
    [Serializable]
    public class BizObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BizObject()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bizSchema"></param>
        public BizObject(BizSchema bizSchema)
        {
            this.Schema = bizSchema;
        }

        private string _SchemaCode = null;
        /// <summary>
        /// 获取或设置业务对象编码
        /// </summary>
        public string SchemaCode
        {
            get
            {
                return this._SchemaCode;
            }
            set
            {
                this._SchemaCode = value;
            }
        }

        /// <summary>
        /// 获取或设置业务对象的值
        /// </summary>
        public string ObjectId
        {
            get
            {
                return this[EntityBase.PropertyName_ObjectId] + string.Empty;
            }
            set
            {
                if (this.ValueTables.ContainsKey(EntityBase.PropertyName_ObjectId))
                {
                    this[EntityBase.PropertyName_ObjectId] = value;
                }
            }
        }
        
        public DateTime CreatedTime
        {
            get
            {
                if (this[BizSchema.PropertyName_CreatedTime] != null)
                {
                    return DateTime.Parse(this[BizSchema.PropertyName_CreatedTime] + string.Empty);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                if (this.ValueTables.ContainsKey(EntityBase.PropertyName_CreatedTime))
                {
                    this[EntityBase.PropertyName_CreatedTime] = value;
                }
            }
        }

        public string CreatedBy
        {
            get
            {
                return this[EntityBase.PropertyName_CreateBy] + string.Empty;
            }
            set
            {
                if (this.ValueTables.ContainsKey(EntityBase.PropertyName_CreateBy))
                {
                    this[EntityBase.PropertyName_CreatedTime] = value;
                }
            }
        }

        private BizSchema _Schema = null;
        /// <summary>
        /// 获取或设置Schema
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public BizSchema Schema
        {
            get { return this._Schema; }
            set
            {
                this._Schema = value;
                if (value != null)
                {
                    this._SchemaCode = value.SchemaCode;
                    foreach (BizProperty property in value.Properties)
                    {
                        if (!this.ValueTables.ContainsKey(property.PropertyCode))
                        {
                            this.ValueTables.Add(property.PropertyCode, null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置属性的名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [NotMapped]
        [JsonIgnore]
        public object this[string name]
        {
            get
            {
                if (!this.ValueTables.ContainsKey(name))
                {
                    throw new Exception(string.Format("Can not get data value which is not exists ->{0}", name));
                }
                if (name.Equals(EntityBase.PropertyName_ObjectId))
                {
                    if (string.IsNullOrWhiteSpace(this.ValueTables[name] + string.Empty))
                    {// ObjectId 是空值，则构造一个默认的
                        this.ValueTables[name] = Guid.NewGuid().ToString().ToLower();
                    }
                }
                return this.ValueTables[name];
            }
            set
            {
                if (!this.ValueTables.ContainsKey(name))
                {
                    throw new Exception(string.Format("Can not set data value which is not exists ->{0}", name));
                }
                this.ValueTables[name] = value;
            }
        }

        private Dictionary<string, object> _ValueTables = null;
        /// <summary>
        /// 获取或设置属性和值的集合
        /// </summary>
        public Dictionary<string, object> ValueTables
        {
            get
            {
                if (this._ValueTables == null)
                {
                    this._ValueTables = new Dictionary<string, object>();
                }
                return this._ValueTables;
            }
        }

        /// <summary>
        /// 获取数据项的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">数据项名称</param>
        /// <returns></returns>
        public T GetValue<T>(string name)
        {
            return (T)this[name];
        }

        /// <summary>
        /// 获取数据项的值
        /// </summary>
        /// <param name="name">数据项名称</param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            return this[name];
        }

        /// <summary>
        /// 设置数据项的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string name, object value)
        {
            this[name] = value;
            return true;
        }

        /// <summary>
        /// 重置业务ID
        /// </summary>
        public void ResetObjectId()
        {
            this.ObjectId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 转换为String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }

}
