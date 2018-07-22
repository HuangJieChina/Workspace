using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using DapperExtensions.Mapper;
using HH.API.Entity.Database;

namespace HH.API.Entity.App
{
    /// <summary>
    /// 应用包定义
    /// </summary>
    [Serializable]
    public class AppPackage : EntityBase
    {
        public const string PropertyName_AppCode = "AppCode";
        /// <summary>
        /// 获取或设置应用包编码
        /// </summary>
        [StringLength(200, MinimumLength = 1)]
        [Required]
        public string AppCode { get; set; }

        /// <summary>
        /// 获取或设置应用名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 获取或设置绑定的钉钉应用Id
        /// </summary>
        public string DingtalkAppId { get; set; }

        /// <summary>
        /// 获取或设置绑定的微信应用Id
        /// </summary>
        public string WeChatAppId { get; set; }

        /// <summary>
        /// 获取或设置应用包排序号
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 获取或设置应用图标URL地址
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 获取或设置是否默认应用
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 获取或设置应用是否启用状态
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.AppPackage;
            }
        }

    }
}