using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Entity.BizObject
{
    /// <summary>
    /// 所有实体类的基类
    /// </summary>
    [Serializable]
    public class BizObject
    {
        /// <summary>
        /// 获取或设置业务对象编码
        /// </summary>
        public string SchemaCode { get; set; }

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
                this[EntityBase.PropertyName_ObjectId] = value;
            }
        }

        /// <summary>
        /// 获取或设置Schema
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public BizSchema Schema { get; set; }

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
                    throw new Exception(string.Format("不能获取不存在属性的值->{0}", name));
                }
                return this.ValueTables[name];
            }
            set
            {
                if (!this.ValueTables.ContainsKey(name))
                {
                    throw new Exception(string.Format("不能设置不存在属性的值->{0}", name));
                }
                this.ValueTables[name] = value;
            }
        }

        /// <summary>
        /// 获取或设置属性和值的集合
        /// </summary>
        public Dictionary<string, object> ValueTables { get; set; }

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
        /// 设置数据项的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue<T>(string name, object value)
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
