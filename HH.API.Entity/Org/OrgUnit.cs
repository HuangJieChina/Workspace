using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DapperExtensions.Mapper;

namespace HH.API.Entity
{
    /// <summary>
    /// 组织机构类
    /// </summary>
    [Serializable]
    [Table(EntityConfig.Table.OrgUnit)]
    public class OrgUnit : EntityBase
    {
        /// <summary>
        /// 获取或设置组织名称
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置上级组织Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 从其他系统同步时，绑定的源Id
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 获取或设置在上级组织中的排序键
        /// </summary>
        public int SortKey { get; set; }

        /// <summary>
        /// 获取或设置当前组织的经理
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// 获取或设置分管领导
        /// </summary>
        public string AssignedLeaderId { get; set; }

        /// <summary>
        /// 获取或设置当前组织是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置描述信息
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// 测试使用属性，可忽略
        /// </summary>
        [NotMapped]
        public string FullName { get { return this.DisplayName; } }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.OrgUnit;
            }
        }
    }

}