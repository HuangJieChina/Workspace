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

        public const string PropertyName_Code = "Code";
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
        public string RoleName { get; set; }

        /// <summary>
        /// 获取或设置是否激活
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 获取或设置角色类型
        /// </summary>
        public OrgRoleType RoleType { get; set; }

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
                return EntityConfig.Table.OrgRole;
            }
        }
    }

}