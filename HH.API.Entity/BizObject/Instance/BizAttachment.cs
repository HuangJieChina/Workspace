using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using DapperExtensions.Mapper;

namespace HH.API.Entity
{
    /// <summary>
    /// 流程实例表
    /// </summary>
    [Serializable]
    public class BizInstance : EntityBase
    {
        /// <summary>
        /// 获取或设置流程实例名称
        /// </summary>
        [StringLength(200)]
        [Required]
        public string InstanceName { get; set; }

        /// <summary>
        /// 获取或设置流程审批结果
        /// </summary>
        public bool Approval { get; set; }

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

    public enum InstanceState
    {
        /// <summary>
        /// 运行中
        /// </summary>
        Running = 1,
        /// <summary>
        /// 已结束
        /// </summary>
        Finished = 2,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 3,
        /// <summary>
        /// 挂起状态(只有运行中的才能被挂起，挂起后不出现在待办中，也不适用于设置策略)
        /// </summary>
        Suspended = 4
    }
}