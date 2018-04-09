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
    public class User : EntityBase
    {
        /// <summary>
        /// 获取或设置用户登录帐号
        /// </summary>
        [StringLength(64, MinimumLength = 3)]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置用户显示名称
        /// </summary>
        [StringLength(64, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置用户所属组织Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 获取或设置用户的密码
        /// </summary>
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置用户的上级经理
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// 获取或设置用户是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置是否系统用户
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 获取或设置是否虚拟用户
        /// </summary>
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 获取或设置虚拟用户关联的真实用户Id
        /// </summary>
        public bool RelationUserId { get; set; }

        /// <summary>
        /// 获取或设置同步源的用户ID
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 获取或设置用户生日
        /// </summary>
        private DateTime BirthDay { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return EntityConfig.Table_OrgUser;
            }
        }
    }
}