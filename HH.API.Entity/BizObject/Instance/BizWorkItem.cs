using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HH.API.Entity.BizObject
{
    /// <summary>
    /// 任务数据
    /// </summary>
    [Serializable]
    public class BizWorkItem : EntityBase
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
    /// 任务类型
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 待办类型
        /// </summary>
        WorkItem = 0,
        /// <summary>
        /// 协办类型
        /// </summary>
        Assist = 1,
        /// <summary>
        /// 传阅类型
        /// </summary>
        CirculateItem = 2
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum ItemState
    {
        Wating = 0,
        Working = 1,
        Finished = 2,
        Cancel = 3
    }

    /// <summary>
    /// 待办任务
    /// </summary>
    [Serializable]
    public class BizWorkItemUnfinished : BizWorkItem
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
    public class BizWorkItemFinished : BizWorkItem
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
    public class BizCirculateItemFinished : BizWorkItem
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
    /// 待阅任务
    /// </summary>
    [Serializable]
    public class BizCirculateItemUnfinished : BizWorkItem
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
}