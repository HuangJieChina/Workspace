﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Entity.BizModel
{
    /// <summary>
    /// 流程模型表
    /// </summary>
    [Serializable]
    public class WorkflowTemplate : EntityBase
    {
        public WorkflowTemplate() { }

        /// <summary>
        /// 流程模板构造函数
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="workflowCode"></param>
        /// <param name="displayName"></param>
        /// <param name="createdBy"></param>
        /// <param name="sortOrder"></param>
        public WorkflowTemplate(string schemaCode, string workflowCode, string displayName,
            string createdBy, int sortOrder)
        {
            this.SchemaCode = schemaCode;
            this.WorkflowCode = workflowCode;
            this.DisplayName = displayName;
            this.CreatedBy = createdBy;
            this.SortOrder = sortOrder;
            this.WorkflowState = WorkflowState.Design;
        }

        public const string PropertyName_WorkflowCode = "WorkflowCode";
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
        /// 获取或设置流程模板版本号
        /// </summary>
        public int WorkflowVersion { get; set; }

        /// <summary>
        /// 获取或设置流程模板状态
        /// </summary>
        public WorkflowState WorkflowState { get; set; }

        /// <summary>
        /// 获取或设置流程模板内容
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public WorkflowTemplateDefine WorkflowTemplateContent { get; private set; }

        /// <summary>
        /// 获取或设置JSON格式的内容
        /// </summary>
        public string ContentText
        {
            get
            {
                return JsonConvert.SerializeObject(this.WorkflowTemplateContent);
            }
            set
            {
                this.WorkflowTemplateContent = JsonConvert.DeserializeObject<WorkflowTemplateDefine>(value);
            }
        }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        [NotMapped]
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.BizWorkflowTemplate;
            }
        }

        /// <summary>
        /// 转换为String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("SchemaCode={0},WorkflowCode={1},DisplayName={2}",
                this.SchemaCode, this.WorkflowCode, this.DisplayName);
        }

        // End Class
    }

    public class WorkflowTemplateDefine
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

   
}