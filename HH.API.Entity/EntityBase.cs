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

        }

        /// <summary>
        /// 获取数据库表名称
        /// </summary>
        public abstract string TableName { get; }

        /// <summary>
        /// 获取或设置主键字段
        /// </summary>
        [Key]
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ObjectId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }

        public override string ToString()
        {
            return this.GetType().FullName + ",ObjectID=" + this.ObjectId;
            // return base.ToString();
        }
    }
}
