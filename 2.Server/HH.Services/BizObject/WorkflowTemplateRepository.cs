using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IServices;
using HH.API.Entity.BizModel;

namespace HH.API.Services
{
    public class WorkflowTemplateRepository : RepositoryBase<WorkflowTemplate>,
        IWorkflowTemplateRepository
    {
        public WorkflowTemplateRepository() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        public List<WorkflowTemplate> GetPublishedWorkflowTemplates(string workflowCode)
        {
            return this.GetListByKey(WorkflowTemplate.PropertyName_WorkflowCode, workflowCode);
        }

        /// <summary>
        /// 获取正在设计的流程模板的流程模板
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        public WorkflowTemplate GetDesignWorkflowTemplate(string workflowCode)
        {
            IList<IPredicate> predList = new List<IPredicate>();
            predList.Add(Predicates.Field<WorkflowTemplate>(p => p.WorkflowCode, Operator.Eq, workflowCode));
            predList.Add(Predicates.Field<WorkflowTemplate>(p => p.WorkflowState, Operator.Eq, (int)WorkflowState.Design));
            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            return this.GetSingle(predGroup);
        }

        /// <summary>
        /// 获取正在运行版本的流程模板
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        public WorkflowTemplate GetDefaultWorkflowTemplate(string workflowCode)
        {
            WorkflowTemplate template = this.GetDesignWorkflowTemplate(workflowCode);

            return this.GetWorkflowTemplate(workflowCode, template.WorkflowVersion);
        }

        /// <summary>
        /// 根据版本号获取
        /// </summary>
        /// <param name="workflowCode"></param>
        /// <param name="workflowVersion"></param>
        /// <returns></returns>
        public WorkflowTemplate GetWorkflowTemplate(string workflowCode, int workflowVersion)
        {
            IList<IPredicate> predList = new List<IPredicate>();
            predList.Add(Predicates.Field<WorkflowTemplate>(p => p.WorkflowCode, Operator.Eq, workflowCode));
            predList.Add(Predicates.Field<WorkflowTemplate>(p => p.WorkflowVersion, Operator.Eq, workflowVersion));
            predList.Add(Predicates.Field<WorkflowTemplate>(p => p.WorkflowState, Operator.Eq, (int)WorkflowState.Published));
            IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

            return this.GetSingle(predGroup);
        }
    }
}