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
            public const string OrgUnit = "Sys_OrgUnit";
            /// <summary>
            /// 用户表
            /// </summary>
            public const string OrgUser = "Sys_OrgUser";
            /// <summary>
            /// 角色表
            /// </summary>
            public const string OrgRole = "Sys_OrgRole";

            /// <summary>
            /// 角色/用户表
            /// </summary>
            public const string OrgRoleUser = "Sys_OrgRoleUser";

            /// <summary>
            /// Vessel表
            /// </summary>
            public const string Table_VesselConfig = "SysVesselConfig";

            public const string Table_RegisterConfig = "SysRegisterConfig";

            public const string Table_EngineConfig = "SysEngineConfig";
        }
    }

   
    
}