using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using DapperExtensions.Mapper;
using Newtonsoft.Json;

namespace HH.API.Entity.BizObject
{
    /// <summary>
    /// 流程实例表
    /// </summary>
    [Serializable]
    public class BizInstanceContext : EntityBase
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
        /// 获取或设置计划完成时间
        /// </summary>
        public DateTime PlanFinishedTime { get; set; }

        private PriorityType _Priority = PriorityType.Normal;
        /// <summary>
        /// 获取或设置流程优先级
        /// </summary>
        public PriorityType Priority
        {
            get { return this._Priority; }
            set { this._Priority = value; }
        }

        /// <summary>
        /// 获取或设置绑定的业务数据Id
        /// </summary>
        public string BizObjectId { get; set; }

        /// <summary>
        /// 获取或设置业务数据
        /// </summary>
        [NotMapped]
        public BizObject BizObject { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.BizInstanceContext;
            }
        }
    }

    public enum PriorityType
    {
        /// <summary>
        /// 低
        /// </summary>
        Lower = 0,
        /// <summary>
        /// 中
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 高
        /// </summary>
        High = 2
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
        /// 异常状态
        /// </summary>
        Exceptional = 4,
        /// <summary>
        /// 挂起状态(只有运行中的才能被挂起，挂起后不出现在待办中，也不适用于设置策略)
        /// </summary>
        Suspended = 5

    }
}