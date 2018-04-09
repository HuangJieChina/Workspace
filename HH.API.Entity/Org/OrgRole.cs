using HH.API.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.API.Entity
{
    /// <summary>
    /// 角色类
    /// </summary>
    [Serializable]
    public class OrgRole : EntityBase
    {
        /// <summary>
        /// 是否所有人权限
        /// </summary>
        public const string EveryOne = "EveryOne";

        /// <summary>
        /// 获取或设置用户登录帐号
        /// </summary>
        [StringLength(64, MinimumLength = 3)]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置用户显示名称
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置角色类型
        /// </summary>
        public UnitType RoleType { get; set; }

        /// <summary>
        /// 获取是否是全部用户组
        /// </summary>
        [NotMapped]
        public bool IsEveryOne
        {
            get
            {
                return this.Code.Equals(EveryOne, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.OrgUser;
            }
        }
    }

    /// <summary>
    /// 组织类型
    /// </summary>
    public enum UnitType
    {
        /// <summary>
        /// 系统组织
        /// </summary>
        System,
        /// <summary>
        /// 用户组织
        /// </summary>
        User
    }
}