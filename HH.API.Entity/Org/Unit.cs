using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.API.Entity.Org
{
    /// <summary>
    /// 组织机构类
    /// </summary>
    [Serializable]
    [Table(EntityConfig.Table_OrgUnit)]
    public class Unit : EntityBase
    {
        /// <summary>
        /// 获取或设置组织名称
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
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

        public override string TableName
        {
            get
            {
                return EntityConfig.Table_OrgUnit;
            }
        }
    }
}
