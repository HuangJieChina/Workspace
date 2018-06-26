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
            public const string OrgUnit = "OrgUnit";
            /// <summary>
            /// 用户表
            /// </summary>
            public const string OrgUser = "OrgUser";
            /// <summary>
            /// 角色表
            /// </summary>
            public const string OrgRole = "OrgRole";

            /// <summary>
            /// 用户组
            /// </summary>
            public const string OrgGroup = "OrgGroup";

            /// <summary>
            /// 角色/用户表
            /// </summary>
            public const string OrgRoleUser = "OrgRoleUser";

            /// <summary>
            /// 业务对象结构表
            /// </summary>
            public const string BizSchema = "BizSchema";

            /// <summary>
            /// 业务表单表
            /// </summary>
            public const string BizSheet = "BizSheet";

            /// <summary>
            /// 业务对象属性
            /// </summary>
            public const string BizProperty = "BizProperty";
            /// <summary>
            /// 业务数据附件表
            /// </summary>
            public const string BizAttachment = "BizAttachment";
            /// <summary>
            /// 业务数据审批意见表
            /// </summary>
            public const string BizComment = "BizComment";

            /// <summary>
            /// 应用包
            /// </summary>
            public const string AppPackage = "AppPackage";

            /// <summary>
            /// 流程包
            /// </summary>
            public const string BizWorkflowPackage = "BizWorkflowPackage";

            public const string BizInstanceContext = "BizInstanceContext";

            /// <summary>
            /// 活动表名称
            /// </summary>
            public const string BizToken = "BizToken";

            public const string BizWorkItemTrack = "BizWorkItemTrack";

            public const string BizWorkItemUnFinished = "BizWorkItemUnFinished";

            public const string BizWorkItemFinished = "BizWorkItemFinished";

            /// <summary>
            /// 目录表
            /// </summary>
            public const string SysFunctionNode = "SysFunctionNode";

            /// <summary>
            /// Vessel表
            /// </summary>
            public const string Table_VesselConfig = "SysVesselConfig";

            public const string Table_RegisterConfig = "SysRegisterConfig";

            public const string Table_EngineConfig = "SysEngineConfig";
        }
    }



}