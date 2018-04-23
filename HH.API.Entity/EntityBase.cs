using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Entity
{
    /// <summary>
    /// 所有实体类的基类
    /// </summary>
    [Serializable]
    public abstract class EntityBase
    {
        /// <summary>
        /// 基类构造函数
        /// </summary>
        public EntityBase()
        {
            this.CreatedTime = DateTime.Now;
            this.ModifiedTime = DateTime.Now;
        }

        /// <summary>
        /// 基类构造函数
        /// </summary>
        /// <param name="objectId"></param>
        public EntityBase(string objectId) : base()
        {
            this.ObjectId = ObjectId;
        }

        /// <summary>
        /// 获取数据库表名称
        /// </summary>
        public abstract string TableName { get; }

        /// <summary>
        /// ObjectID 字段名称
        /// </summary>
        public const string PropertyName_ObjectId = "ObjectId";

        private string _ObjectId = null;

        /// <summary>
        /// 获取或设置主键字段
        /// </summary>
        [Key]
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ObjectId
        {
            get
            {
                if (this._ObjectId == null)
                {
                    this._ObjectId = Guid.NewGuid().ToString();
                }
                return this._ObjectId;
            }
            set { this._ObjectId = value; }
        }

        /// <summary>
        /// 获取或设置创建人
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string CreateBy { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置修改人
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 获取或设置修改时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// 获取或设置扩展字段1
        /// </summary>
        public string ExtendField1 { get; set; }

        /// <summary>
        /// 获取或设置扩展字段2
        /// </summary>
        public string ExtendField2 { get; set; }

        /// <summary>
        /// 获取或设置扩展字段3
        /// </summary>
        public string ExtendField3 { get; set; }

        /// <summary>
        /// 转换为String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.GetType().FullName + ",ObjectID=" + this.ObjectId;
            // return base.ToString();
        }
    }
}
