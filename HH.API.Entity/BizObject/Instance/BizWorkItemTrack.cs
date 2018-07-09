using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HH.API.Entity.BizObject.Instance
{
    /// <summary>
    /// 任务转交记录表
    /// </summary>
    [Serializable]
    public class WorkItemTrack : EntityBase
    {
        /// <summary>
        /// 获取或设置工作任务Id
        /// </summary>
        public string WorkItemId { get; set; }

        /// <summary>
        /// 获取或设置转交人
        /// </summary>
        public string Transfer { get; set; }

        /// <summary>
        /// 获取或设置接收人
        /// </summary>
        public string Recipientor { get; set; }

        /// <summary>
        /// 获取或设置转交意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.WorkItemTrack;
            }
        }
    }
}
