using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Entity
{
    /// <summary>
    /// 流程模型表
    /// </summary>
    [Serializable]
    public class WorkflowTemplate : EntityBase
    {
        /// <summary>
        /// 获取或设置所属业务模型编码
        /// </summary>
        public string WorkflowCode { get; set; }

        /// <summary>
        /// 获取或设置流程模板显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置所属业务模型编码
        /// </summary>
        public string SchemaCode { get; set; }

        /// <summary>
        /// 获取或设置表单排序值
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 获取或设置流程模板内容
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public WorkflowTemplateContent WorkflowTemplateContent { get; private set; }

        /// <summary>
        /// 获取或设置JSON格式的内容
        /// </summary>
        public string Content
        {
            get
            {
                return JsonConvert.SerializeObject(this.WorkflowTemplateContent);
            }
            set
            {
                this.WorkflowTemplateContent = JsonConvert.DeserializeObject<WorkflowTemplateContent>(value);
            }
        }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.BizSchema;
            }
        }

        /// <summary>
        /// 转换为String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        // End Class
    }


    public class WorkflowTemplateContent
    {
        /// <summary>
        /// 获取或设置流程实例名称公式
        /// </summary>
        public string InstanceNameExpression { get; set; }

        /// <summary>
        /// 获取或设置描述信息
        /// </summary>
        public string Description { get; set; }

        public List<ActivityTemplate> Activies { get; set; }
    }

    /// <summary>
    /// 节点模板信息
    /// </summary>
    public class ActivityTemplate
    {
        /// <summary>
        /// 获取或设置节点类型
        /// </summary>
        public ActivityType ActivityType { get; set; }
        /// <summary>
        /// 获取或设置活动节点编码
        /// </summary>
        public string ActivityCode { get; set; }

        /// <summary>
        /// 获取或设置活动节点名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 获取或设置左位移
        /// </summary>
        public decimal Left { get; set; }

        /// <summary>
        /// 获取或设置上位移
        /// </summary>
        public decimal Top { get; set; }

        /// <summary>
        /// 获取或设置宽度
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// 获取或设置高度
        /// </summary>
        public decimal Height { get; set; }
    }

    /// <summary>
    /// 活动节点类型
    /// </summary>
    public enum ActivityType
    {
        /// <summary>
        /// 用户活动
        /// </summary>
        User = 0,
        /// <summary>
        /// 系统活动
        /// </summary>
        System = 1,
        /// <summary>
        /// 连接点
        /// </summary>
        Connect = 2,
        /// <summary>
        /// 传阅活动
        /// </summary>
        Circulate = 3,
        /// <summary>
        /// 子流程
        /// </summary>
        SubWorkflow = 4
    }
}