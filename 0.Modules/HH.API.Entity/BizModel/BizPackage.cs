using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using DapperExtensions.Mapper;

namespace HH.API.Entity.BizModel
{
    /// <summary>
    /// 业务模型
    /// </summary>
    [Serializable]
    public class BizPackage : EntityBase
    {
        public BizPackage() { }

        public BizPackage(string catalogId,
            string packageCode,
            string packageName,
            int sortOrder)
        {
            this.CatalogId = catalogId;
            this.PackageCode = packageCode;
            this.PackageName = packageName;
            this.SortOrder = sortOrder;
        }

        public const string PropertyName_PackageCode = "PackageCode";

        /// <summary>
        /// 获取或设置流程包编码
        /// </summary>
        [StringLength(128, MinimumLength = 3)]
        [Required]
        public string PackageCode { get; set; }

        /// <summary>
        /// 获取或设置流程包名称
        /// </summary>
        [Required]
        public string PackageName { get; set; }

        /// <summary>
        /// 获取或设置对应所属目录Id
        /// </summary>
        public string CatalogId { get; set; }

        /// <summary>
        /// 获取或设置应用包排序号
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.BizPackage;
            }
        }

    }
}