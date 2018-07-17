using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HH.API.Entity.Instance
{
    /// <summary>
    /// 任务基类
    /// </summary>
    [Serializable]
    public class WorkItem : EntityBase
    {
        /// <summary>
        /// 获取或设置活动节点编码
        /// </summary>
        public string ActivityCode { get; set; }
        /// <summary>
        /// 获取或设置流程实例Id
        /// </summary>
        public string InstanceId { get; set; }
        /// <summary>
        /// 获取或设置任务处理人
        /// </summary>
        public string Participant { get; set; }
        /// <summary>
        /// 获取或设置代理人(当A委托给B时，这里记录的是B的Id)
        /// </summary>
        public string Agentor { get; set; }

        /// <summary>
        /// 获取或设置任务状态
        /// </summary>
        public ItemState ItemState { get; set; }

        /// <summary>
        /// 获取或设置计划完成时间
        /// </summary>
        public DateTime PlanFinishedTime { get; set; }

        /// <summary>
        /// 获取或设置任务完成时间
        /// </summary>
        public DateTime FinishedTime { get; set; }

        /// <summary>
        /// 获取或设置转交人
        /// </summary>
        public string Transfer { get; set; }

        /// <summary>
        /// 获取或设置转交人姓名
        /// </summary>
        public string TransferName { get; set; }

        [NotMapped]
        [JsonIgnore]
        public override string TableName => throw new NotImplementedException();
    }

    /// <summary>
    /// 待办任务
    /// </summary>
    [Serializable]
    public class WorkItemUnfinished : WorkItem
    {
        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.WorkItemUnFinished;
            }
        }
    }

    /// <summary>
    /// 已办任务
    /// </summary>
    [Serializable]
    public class WorkItemFinished : WorkItem
    {
        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.WorkItemFinished;
            }
        }
    }

    /// <summary>
    /// 已阅任务
    /// </summary>
    [Serializable]
    public class CirculateItemFinished : WorkItem
    {
        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.CirculateItemFinished;
            }
        }
    }

    /// <summary>
    /// 待阅任务
    /// </summary>
    [Serializable]
    public class CirculateItemUnfinished : WorkItem
    {
        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.CirculateItem;
            }
        }
    }
}