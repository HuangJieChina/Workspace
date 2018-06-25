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
    public class OrgUnit : OrganizationObject
    {
        /// <summary>
        /// 获取或设置组织名称
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string UnitName { get; set; }

        /// <summary>
        /// 获取或设置在上级组织中的排序键
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 获取或设置是否根节点
        /// </summary>
        public bool IsRootUnit { get; set; }

        /// <summary>
        /// 获取或设置分管领导
        /// </summary>
        public string AssignedLeaderId { get; set; }

        /// <summary>
        /// 获取或设置描述信息
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }

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