using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using DapperExtensions.Mapper;

namespace HH.API.Entity
{
    /// <summary>
    /// 审批记录表
    /// </summary>
    [Serializable]
    public class BizComment : EntityBase
    {
        /// <summary>
        /// 获取或设置创建者姓名
        /// </summary>
        [StringLength(200, MinimumLength = 1)]
        [Required]
        public string CreatorName { get; set; }

        /// <summary>
        /// 获取或设置委托人Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string DelegatorId { get; set; }

        /// <summary>
        /// 获取或设置委托人姓名
        /// </summary>
        public string DelegatorName { get; set; }

        /// <summary>
        /// 获取或设置审批意见文本
        /// </summary>
        [StringLength(2000)]
        [MaxLength(2000)]
        public string CommentText { get; set; }

        /// <summary>
        /// 获取或设置用户使用的签章Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string SignatureId { get; set; }

        /// <summary>
        /// 获取或设置是否审批同意
        /// </summary>
        public bool Approval { get; set; }

        /// <summary>
        /// 获取或设置工作任务Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string WorkitemId { get; set; }

        /// <summary>
        /// 获取或设置流程实例Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string InstanceId { get; set; }

        /// <summary>
        /// 获取或设置绑定的数据项名称
        /// </summary>
        [StringLength(64)]
        public string PropertyName { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.BizComment;
            }
        }

    }
}