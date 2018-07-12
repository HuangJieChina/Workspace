using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.BizModel;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    public interface IWorkflowTemplateRepository : IRepositoryBase<WorkflowTemplate>
    {
        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        WorkflowTemplate GetDesignWorkflowTemplate(string workflowCode);

        /// <summary>
        /// 获取默认的已发布版本流程模板
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        WorkflowTemplate GetDefaultWorkflowTemplate(string workflowCode);

        /// <summary>
        /// 获取指定版本的流程模板
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <param name="workflowVersion"></param>
        /// <returns></returns>
        WorkflowTemplate GetWorkflowTemplate(string workflowCode, int workflowVersion);

        /// <summary>
        /// 获取所有已发布的流程模板集合
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        List<WorkflowTemplate> GetPublishedWorkflowTemplates(string workflowCode);
    }
}
