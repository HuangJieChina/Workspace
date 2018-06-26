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
        public FunctionNode() { }

        /// <summary>
        /// 用于新建的构造函数
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="functionName"></param>
        /// <param name="createdBy"></param>
        /// <param name="sortOrder"></param>
        /// <param name="isRoot"></param>
        /// <param name="functionType"></param>
        public FunctionNode(string parentId,
            string functionName,
            string createdBy,
            int sortOrder,
            bool isRoot,
            FunctionType functionType)
        {
            this.ParentId = parentId;
            this.FunctionName = functionName;
            this.CreatedBy = createdBy;
            this.SortOrder = sortOrder;
            this.IsRoot = isRoot;
            this.FunctionType = functionType;
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
        /// 获取或设置是否根节点
        /// </summary>
        public bool IsRoot { get; set; }

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
                return EntityConfig.Table.SysFunctionNode;
            }
        }

    }
}