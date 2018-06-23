using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.API.Entity
{
    /// <summary>
    /// 单点登录系统用户表
    /// </summary>
    [Serializable]
    public class SsoSystem : EntityBase
    {
        public SsoSystem() { }

        public const string PropertyName_CorpId = "CorpId";
        /// <summary>
        /// 获取或设置系统编码
        /// </summary>
        [Required]
        public string CorpId { get; set; }

        public const string PropertyName_Secret = "Secret";
        /// <summary>
        /// 获取或设置系统秘钥
        /// </summary>
        [StringLength(64, MinimumLength = 6, ErrorMessage = "秘钥最小长度不低于6位")]
        [Required]
        public string Secret { get; set; }

        /// <summary>
        /// 获取或设置系统默认转向的Url地址
        /// </summary>
        [StringLength(512)]
        public string DefaultUrl { get; set; }

        /// <summary>
        /// 获取或设置是否有获取Token的权限
        /// 如果有，则有权限从当前系统跳转至其他实现单点登录的业务系统
        /// </summary>
        public bool AllowGetToken { get; set; }

        /// <summary>
        /// 获取或设置描述说明
        /// </summary>
        [StringLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置是否激活状态
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 获取或设置显示顺序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 设置秘钥
        /// </summary>
        /// <param name="secret"></param>
        public void SetSecret(string secret)
        {
            this.Secret = this.GetMD5Encrypt32(secret);
        }

        /// <summary>
        /// 检查秘钥是否匹配
        /// </summary>
        /// <param name="secret">传入的秘钥</param>
        /// <returns></returns>
        public bool ValidateSecret(string secret)
        {
            return this.GetMD5Encrypt32(secret).Equals(this.Secret);
        }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.OrgRoleUser;
            }
        }

    }
}