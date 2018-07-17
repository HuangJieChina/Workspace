using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using DapperExtensions.Mapper;

namespace HH.API.Entity.App
{
    /// <summary>
    /// 应用包程序目录
    /// </summary>
    [Serializable]
    public class AppCatalog : EntityBase
    {
        public AppCatalog() { }

        /// <summary>
        /// 用于新建的构造函数
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="functionName"></param>
        /// <param name="createdBy"></param>
        /// <param name="sortOrder"></param>
        /// <param name="isRoot"></param>
        public AppCatalog(string parentId,
            string functionName,
            string createdBy,
            int sortOrder)
        {
            this.ParentId = parentId;
            this.FunctionName = functionName;
            this.CreatedBy = createdBy;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// 获取或设置目录显示名称
        /// </summary>
        public string FunctionName { get; set; }

        public const string PropertyName_SortOrder = "SortOrder";
        /// <summary>
        /// 获取或设置应用包排序号
        /// </summary>
        public int SortOrder { get; set; }

        public const string PropertyName_ParentId = "ParentId";
        /// <summary>
        /// 获取或设置上级目录Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 获取或设置是否在应用中显示当前目录
        /// </summary>
        public bool IsDisplay { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.AppCatalog;
            }
        }

    }
}