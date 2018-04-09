using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.API.Entity
{
    /// <summary>
    /// 组织机构类
    /// </summary>
    [Serializable]
    public class OrgUser : EntityBase
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
        /// 获取或设置用户的密码(MD5加密)
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
        /// <para>注：系统用户不能在前端选人界面中展示、不参与业务，只做系统管理</para>
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 获取或设置是否虚拟用户
        /// <para>注：虚拟用户必须绑定至真实用户账号</para>
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
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 获取或设置用户性别,男=0，女=1
        /// </summary>
        public int Gender { get; set; }

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
}