using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using DapperExtensions.Mapper;

namespace HH.API.Entity.BizData
{
    /// <summary>
    /// 附件表
    /// </summary>
    [Serializable]
    public class BizAttachment : EntityBase
    {
        /// <summary>
        /// 获取或设置文件名称
        /// </summary>
        [StringLength(200, MinimumLength = 1)]
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置附件文件扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 获取或设置附件文件大小
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// 获取或设置附件文件存储内容
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// 获取或设置文件存储路径
        /// </summary>
        public string StoragePath { get; set; }

        /// <summary>
        /// 获取或设置文件下载URL地址
        /// </summary>
        public string DownloadUrl { get; set; }

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