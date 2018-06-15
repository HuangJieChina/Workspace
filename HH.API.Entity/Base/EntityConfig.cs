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
            /// 角色/用户表
            /// </summary>
            public const string OrgRoleUser = "OrgRoleUser";

            /// <summary>
            /// 业务对象结构表
            /// </summary>
            public const string BizSchema = "BizSchema";
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
            /// 应用包
            /// </summary>
            public const string BizWorkflowPackage = "BizWorkflowPackage";


            /// <summary>
            /// Vessel表
            /// </summary>
            public const string Table_VesselConfig = "SysVesselConfig";

            public const string Table_RegisterConfig = "SysRegisterConfig";

            public const string Table_EngineConfig = "SysEngineConfig";
        }
    }



}