using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.API.Entity.Orgainzation
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
        /// 获取或设置用户的职务显示名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// 获取或设置员工职级
        /// </summary>
        public int EmployeeRank { get; set; }

        /// <summary>
        /// 获取或设置用户办公地点
        /// </summary>
        public string OfficePlace { get; set; }

        /// <summary>
        /// 获取或设置用户手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置用户手机号2(仅做记录)
        /// </summary>
        public string Mobile2 { get; set; }

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
        [StringLength(64, MinimumLength = 3)]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置虚拟用户关联的真实用户Id
        /// </summary>
        [StringLength(36)]
        [Column(TypeName = "char")]
        public string RelationUserId { get; set; }

        /// <summary>
        /// 获取或设置用户生日
        /// </summary>
        public DateTime BirthDay { get; set; } = new DateTime(1980, 1, 1);

        /// <summary>
        /// 获取或设置用户入职日期
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// 获取或设置用户离职日期
        /// </summary>
        public DateTime LeaveDate { get; set; }

        /// <summary>
        /// 获取或设置用户性别,男=0,女=1
        /// </summary>
        public UserGender Gender { get; set; } = UserGender.Male;

        /// <summary>
        /// 获取或设置是否系统用户
        /// <para>注：系统用户不能在前端选人界面中展示、不参与业务，只做系统管理</para>
        /// </summary>
        public bool IsSystemUser { get; set; }

        /// <summary>
        /// 获取或设置是否虚拟用户
        /// <para>注：虚拟用户必须绑定至真实用户账号</para>
        /// </summary>
        public bool IsVirtualUser { get; set; }

        /// <summary>
        /// 获取或设置用户是否超级管理员
        /// </summary>
        /// <para>全局超级管理员只有1人，从钉钉或企业微信中同步过来</para>
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// 获取或设置用户隐私保护级别
        /// </summary>
        /// <para>手机、邮箱、办公电话都为隐私保护信息</para>
        public UserProtectionLevel ProtectionLevel
        {
            get; set;
        } = UserProtectionLevel.OpenToAll;

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
        /// 获取当前组织对象类型：用户
        /// </summary>
        public override OrganizationType OrganizationType => OrganizationType.OrgUser;

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
    /// 用户性别
    /// </summary>
    public enum UserGender
    {
        /// <summary>
        /// 男性
        /// </summary>
        Male = 0,
        /// <summary>
        /// 女性
        /// </summary>
        Female = 1,
        /// <summary>
        /// 未知
        /// </summary>
        Unknow = 2
    }

    /// <summary>
    /// 用户信息隐私保护级别
    /// </summary>
    public enum UserProtectionLevel
    {
        /// <summary>
        /// 对所有人开放
        /// </summary>
        OpenToAll = 0,
        /// <summary>
        /// 对本部门及上级部门开放
        /// </summary>
        OpenToParent = 1,
        /// <summary>
        /// 仅对自己开放
        /// </summary>
        OnlySelf = 2
    }
}