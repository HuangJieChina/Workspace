using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.API.Entity
{
    /// <summary>
    /// 组织用户类
    /// </summary>
    [Serializable]
    public class OrgUser : OrganizationObject
    {
        public const string PropertyName_Code = "Code";
        /// <summary>
        /// 获取或设置用户登录帐号
        /// </summary>
        [StringLength(64, MinimumLength = 3, ErrorMessage = "用户编码在3到64个字符之间")]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置用户中文名称
        /// </summary>
        [StringLength(64, MinimumLength = 1)]
        [Required(ErrorMessage = "用户中文名称不允许为空")]
        public string CnName { get; set; }

        /// <summary>
        /// 获取或设置用户英文名称
        /// </summary>
        [StringLength(64, MinimumLength = 1)]
        public string EnName { get; set; }

        /// <summary>
        /// 获取或设置职务显示名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// 获取或设置员工职级
        /// </summary>
        public decimal EmployeeRank { get; set; }

        /// <summary>
        /// 获取或设置用户手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置用户邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置钉钉用户Id
        /// </summary>
        public string DingTalkId { get; set; }

        /// <summary>
        /// 获取或设置用户微信Id
        /// </summary>
        public string WeChatId { get; set; }

        /// <summary>
        /// 获取或设置用户的密码(MD5加密)
        /// </summary>
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置虚拟用户关联的真实用户Id
        /// </summary>
        public bool RelationUserId { get; set; }

        private DateTime _BirthDay = new DateTime(1980, 1, 1);
        /// <summary>
        /// 获取或设置用户生日
        /// </summary>
        public DateTime BirthDay
        {
            get { return this._BirthDay; }
            set { this._BirthDay = value; }
        }

        /// <summary>
        /// 获取或设置用户性别,男=0，女=1
        /// </summary>
        public int Gender { get; set; }

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
        /// 获取或设置用户是否超级管理员
        /// </summary>
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// 设置用户密码
        /// </summary>
        /// <param name="password"></param>
        public void SetPassword(string password)
        {
            this.Password = this.GetMD5Encrypt32(password);
        }

        /// <summary>
        /// 检查密码是否匹配
        /// </summary>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public bool ValidatePassword(string password)
        {
            return this.GetMD5Encrypt32(password).Equals(this.Password);
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
}