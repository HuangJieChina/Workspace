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
    public class BizToken : EntityBase
    {
        /// <summary>
        /// 获取或设置流程实例Id
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// 获取或设置活动节点顺序
        /// </summary>
        public int TokenId { get; set; }

        /// <summary>
        /// 获取或设置活动节点编码
        /// </summary>
        public string ActivityCode { get; set; }

        /// <summary>
        /// 获取或设置活动节点完成时间
        /// </summary>
        public DateTime FinishedTime { get; set; }

        /// <summary>
        /// 获取或设置活动状态
        /// </summary>
        public ItemState TokenState { get; set; }

        /// <summary>
        /// 获取或设置是否允许取回
        /// </summary>
        public bool Retrievable { get; set; }

        /// <summary>
        /// 获取或设置是否出现活动异常
        /// </summary>
        public bool Exceptional { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.BizToken;
            }
        }
    }
}