using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.API.Entity
{
    /// <summary>
    /// 实体类配置表
    /// </summary>
    public struct EntityConfig
    {
        public struct Org
        {
            public const string SystemUserId = "88888888-8888-8888-8888-888888888888";
            public const string SystemOrgId = "66666666-6666-6666-6666-666666666666";
            public const string SystemAdministratorCode = "Administrator";
            public const string SystemAdministratorName = "系统管理员";
        }

        public struct FunctionNode
        {
            public const string WorkflowPackageRootName = "WorkflowPackage";
        }

        /// <summary>
        /// 数据库表名称配置
        /// </summary>
        public struct Table
        {
            /// <summary>
            /// 组织架构表名称
            /// </summary>
            public const string OrgUnit = "sys_OrgUnit";
            /// <summary>
            /// 用户表
            /// </summary>
            public const string OrgUser = "sys_OrgUser";
            /// <summary>
            /// 角色表
            /// </summary>
            public const string OrgRole = "sys_OrgRole";

            /// <summary>
            /// 用户组
            /// </summary>
            public const string OrgGroup = "sys_OrgGroup";

            /// <summary>
            /// 角色/用户表
            /// </summary>
            public const string OrgRoleUser = "sys_OrgRoleUser";

            /// <summary>
            /// 业务对象结构表
            /// </summary>
            public const string BizSchema = "sys_BizSchema";

            /// <summary>
            /// 业务表单表
            /// </summary>
            public const string BizSheet = "sys_BizSheet";

            /// <summary>
            /// 业务对象属性
            /// </summary>
            public const string BizProperty = "sys_BizProperty";
            /// <summary>
            /// 业务数据附件表
            /// </summary>
            public const string BizAttachment = "sys_BizAttachment";
            /// <summary>
            /// 业务数据审批意见表
            /// </summary>
            public const string BizComment = "sys_BizComment";

            /// <summary>
            /// 应用包
            /// </summary>
            public const string AppPackage = "sys_AppPackage";

            /// <summary>
            /// 流程包
            /// </summary>
            public const string WorkflowPackage = "sys_WorkflowPackage";

            /// <summary>
            /// 流程模板表
            /// </summary>
            public const string WorkflowTemplate = "sys_WorkflowTemplate";

            public const string InstanceContext = "sys_InstanceContext";

            /// <summary>
            /// 活动表名称
            /// </summary>
            public const string Token = "sys_InsToken";
            public const string WorkItemTrack = "sys_InsWorkItemTrack";
            /// <summary>
            /// 未完成任务表
            /// </summary>
            public const string WorkItemUnFinished = "sys_InsWorkItemUnFinished";
            /// <summary>
            /// 已完成任务表
            /// </summary>
            public const string WorkItemFinished = "sys_InsWorkItemFinished";

            /// <summary>
            /// 目录表
            /// </summary>
            public const string SysFunctionNode = "sys_FunctionNode";

            /// <summary>
            /// Vessel表
            /// </summary>
            public const string Table_VesselConfig = "sys_VesselConfig";

            public const string Table_RegisterConfig = "sys_RegisterConfig";

            public const string Table_EngineConfig = "sys_EngineConfig";
        }
    }



}