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
    public class AppFunction : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public AppFunction() { }

        /// <summary>
        /// 用于新建的构造函数
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="functionName"></param>
        /// <param name="createdBy"></param>
        /// <param name="sortOrder"></param>
        /// <param name="isRoot"></param>
        public AppFunction(string parentId,
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
        /// 获取或设置所属的应用Id(冗余字段)
        /// </summary>
        [Required]
        public string AppPackageId { get; set; }

        public const string PropertyName_FunctionCode = "FunctionCode";
        /// <summary>
        /// 获取或设置应用菜单编码(必须唯一)
        /// </summary>
        public string FunctionCode { get; set; }

        /// <summary>
        /// 获取或设置目录显示名称
        /// </summary>
        public string FunctionName { get; set; }

        public const string PropertyName_SortOrder = "SortOrder";
        /// <summary>
        /// 获取或设置应用菜单排序号
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
        /// 获取或设置打开的URL链接
        /// </summary>
        [StringLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置图标URL地址
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 获取或设置图标显示样式
        /// </summary>
        public string IconCss { get; set; }

        /// <summary>
        /// 获取或设置图标显示类型
        /// </summary>
        public IconType IconType { get; set; }

        /// <summary>
        /// 获取或设置链接打开方式
        /// </summary>
        public LinkTarget LinkTarget { get; set; }

        /// <summary>
        /// 获取或设置功能菜单类型
        /// </summary>
        public FunctionType FunctionType { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.AppFunction;
            }
        }
    }
}