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

        /// <summary>
        /// 数据库表名称配置(注：表名称长度不允许超过28个字符)
        /// </summary>
        public struct Table
        {
            #region 组织机构 ------------------
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
            /// 岗位表
            /// </summary>
            public const string OrgPost = "sys_OrgPost";

            /// <summary>
            /// 岗位用户表
            /// </summary>
            public const string OrgPostUser = "sys_OrgPostUser";
            #endregion

            #region 应用包  -------------------
            /// <summary>
            /// 应用包
            /// </summary>
            public const string AppPackage = "sys_AppPackage";
            /// <summary>
            /// 应用程序菜单表(包含：目录、应用包、自定义URL、报表)
            /// </summary>
            public const string AppFunction = "sys_AppFunction";

            #endregion

            #region 业务模型 ------------------
            /// <summary>
            /// 业务模型
            /// </summary>
            public const string BizPackage = "sys_BizPackage";
            /// <summary>
            /// 流程模板表
            /// </summary>
            public const string BizWorkflowTemplate = "sys_BizWorkflowTemplate";
            /// <summary>
            /// 业务对象结构表
            /// </summary>
            public const string BizSchema = "sys_BizSchema";
            /// <summary>
            /// 业务对象属性
            /// </summary>
            public const string BizProperty = "sys_BizProperty";
            /// <summary>
            /// 业务表单表
            /// </summary>
            public const string BizSheet = "sys_BizSheet";
            #endregion

            #region 业务数据 ------------------
            /// <summary>
            /// 流程实例表
            /// </summary>
            public const string InstanceContext = "sys_bizInstanceContext";
            /// <summary>
            /// 业务数据附件表
            /// </summary>
            public const string Attachment = "sys_bizAttachment";
            /// <summary>
            /// 业务数据审批意见表
            /// </summary>
            public const string Comment = "sys_bizComment";
            /// <summary>
            /// 活动表名称
            /// </summary>
            public const string Token = "sys_bizToken";
            /// <summary>
            /// 工作任务交接记录表
            /// </summary>
            public const string WorkItemTrack = "sys_bizWorkItemTrack";
            /// <summary>
            /// 未完成任务表
            /// </summary>
            public const string WorkItemUnFinished = "sys_bizWorkItemUnFinished";
            /// <summary>
            /// 已完成任务表
            /// </summary>
            public const string WorkItemFinished = "sys_bizWorkItemFinished";
            /// <summary>
            /// 待阅表
            /// </summary>
            public const string CirculateItem = "sys_bizCirculateItem";
            /// <summary>
            /// 已阅表
            /// </summary>
            public const string CirculateItemFinished = "sys_bizCirculateItemFinished";
            #endregion

            /// <summary>
            /// Vessel表
            /// </summary>
            public const string Table_VesselConfig = "sys_VesselConfig";

            public const string Table_RegisterConfig = "sys_RegisterConfig";

            public const string Table_EngineConfig = "sys_EngineConfig";
        }
    }
}