using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using DapperExtensions.Mapper;

namespace HH.API.Entity
{
    /// <summary>
    /// 目录定义
    /// </summary>
    [Serializable]
    public class FunctionNode : EntityBase
    {
        /// <summary>
        /// 获取或设置目录显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置应用包排序号
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 获取或设置上级目录Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 获取或设置功能目录类型
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
                return EntityConfig.Table.AppPackage;
            }
        }

    }
}