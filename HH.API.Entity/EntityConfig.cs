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
        /// 组织架构表名称
        /// </summary>
        public const string Table_OrgUnit = "Sys_OrgUnit";
        /// <summary>
        /// 用户表
        /// </summary>
        public const string Table_OrgUser = "Sys_OrgUser";
        /// <summary>
        /// 角色表
        /// </summary>
        public const string Table_OrgRole = "Sys_OrgRole";

        /// <summary>
        /// Vessel表
        /// </summary>
        public const string Table_VesselConfig = "SysVesselConfig";

        public const string Table_RegisterConfig = "SysRegisterConfig";

        public const string Table_EngineConfig = "SysEngineConfig";
    }

    /// <summary>
    /// 调用接口返回编码
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 返回成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 返回未知错误
        /// </summary>
        CodeDuplicate = 1
    }
}