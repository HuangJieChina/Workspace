using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DapperExtensions.Mapper;

namespace HH.API.Entity.Orgainzation
{
    /// <summary>
    /// 组织机构类
    /// </summary>
    [Serializable]
    [Table(EntityConfig.Table.OrgUnit)]
    public class OrgUnit : OrganizationObject
    {
        /// <summary>
        /// 根节点
        /// </summary>
        public const string PropertyName_IsRootUnit = "IsRootUnit";

        /// <summary>
        /// 获取或设置是否根节点
        /// </summary>
        public bool IsRootUnit { get; set; }

        /// <summary>
        /// 获取或设置分管领导
        /// </summary>
        public string AssignedLeaderId { get; set; }

        /// <summary>
        /// 获取或设置当前组织归属的成本中心编码
        /// </summary>
        public string CostCenter { get; set; }

        /// <summary>
        /// 获取或设置描述信息
        /// </summary>
        [StringLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置是否是系统部门
        /// </summary>
        /// <para>系统部门不会在前端组织机构中显示</para>
        public bool IsSystemUnit { get; set; }

        /// <summary>
        /// 获取当前组织对象类型：组织单元
        /// </summary>
        public override OrganizationType OrganizationType => OrganizationType.OrgUnit;

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

    /// <summary>
    /// 组织保护级别
    /// </summary>
    public enum UnitProtectionLevel
    {
        /// <summary>
        /// 对所有人开放
        /// </summary>
        OpenToAll = 0,
        /// <summary>
        /// 对本部门及上级部门开放
        /// </summary>
        OpenToParent = 1
    }
}