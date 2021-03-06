﻿using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.Orgainzation;
using System;
using System.Collections.Generic;

namespace HH.API.IService
{
    /// <summary>
    /// 岗位
    /// </summary>
    public interface IOrganizationService : IServiceBase
    {
        /// <summary>
        /// 根据Id获取当前组织所在的部门
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        OrgDepartment GetOrgDepartmentByUnitId(string objectId);

        #region 组织机构 ----------------------------------
        /// <summary>
        /// 获取跟组织对象
        /// </summary>
        /// <returns></returns>
        OrgDepartment GetRootDepartment();

        /// <summary>
        /// 添加组织单元
        /// </summary>
        /// <param name="orgUnit">组织单元</param>
        /// <returns>返回添加是否成功</returns>
        bool AddOrgDepartment(string userId, OrgDepartment orgUnit);

        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        bool RemoveOrgDepartment(string userId, string objectId);

        /// <summary>
        /// 更新组织信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgUnit"></param>
        /// <returns></returns>
        bool UpdateOrgDepartment(string userId, OrgDepartment orgUnit);

        /// <summary>
        /// 根据上级组织Id获取子组织单元
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="organizationType"></param>
        /// <returns></returns>
        List<OrgUnit> GetChildUnits(string parentId, UnitType organizationType);

        /// <summary>
        /// 设置组织对象是否被启用
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool SetUnitEnabled(string userId, string objectId, bool enabled);

        /// <summary>
        /// 判断一个组织是否另外一个组织的上级
        /// </summary>
        /// <param name="parentId">父组织Id</param>
        /// <param name="childId">子组织Id</param>
        /// <returns></returns>
        bool IsParentUnit(string parentId, string childId);
        #endregion

        #region 用户信息 ----------------------------------
        /// <summary>
        /// 用户密码验证
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ValidPassword(string userCode, string password);

        /// <summary>
        /// 用户密码重置
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ResetPassword(string userId, string objectId, string newPassword);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        bool AddUser(string userId, OrgUser user);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UpdateUser(string userId, OrgUser user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        bool RemoveUser(string userId, string objectId);

        /// <summary>
        /// 根据上级组织ID获取用户
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="recurse"></param>
        /// <returns></returns>
        List<OrgUser> GetChildUsers(string parentId, bool recursive);
        #endregion

        #region  角色信息 ----------------------------------
        /// <summary>
        /// 增加组织角色
        /// </summary>
        /// <param name="orgRole"></param>
        /// <returns></returns>
        bool AddOrgRole(string userId, OrgRole orgRole);

        /// <summary>
        /// 更新组织角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgRole"></param>
        /// <returns></returns>
        bool UpdateOrgRole(string userId, OrgRole orgRole);

        /// <summary>
        /// 删除组织角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        bool RemoveOrgRoleByCode(string userId, string roleCode);
        #endregion

        #region 岗位信息 ----------------------------------
        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="objectId">岗位Id</param>
        /// <returns>返回岗位信息</returns>
        OrgPost GetOrgPost(string objectId);
        #endregion

        #region 找人函数 ----------------------------------
        /// <summary>
        /// 根据角色找人
        /// </summary>
        /// <param name="startOrgId">组织Id</param>
        /// <param name="roleCode">角色编码</param>
        /// <returns></returns>
        List<OrgUser> FindUsersByRoleCode(string startOrgId, string roleCode);

        /// <summary>
        /// 查找指定组织下的所有角色人员
        /// </summary>
        /// <param name="orgUnitId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        List<OrgUser> FindRoleUsersByOrg(string orgUnitId, string roleCode);

        /// <summary>
        /// 获取组织对象的管理者
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns></returns>
        string GetManager(string objectId);

        /// <summary>
        /// 获取组织对象所在的组织单元的管理者
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns></returns>
        string GetOUManager(string objectId);

        /// <summary>
        /// 获取某个特定的组织层级的经理
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="unitLevel"></param>
        /// <returns></returns>
        string GetUnitLevelManager(string objectId, int unitLevel);

        /// <summary>
        /// 获取指定组织往上层级的经理
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="corssLevel"></param>
        /// <returns></returns>
        string GetCrossLevelManager(string objectId, int corssLevel);
        #endregion

        #region 组织同步 ----------------------------------
        /// <summary>
        /// 从钉钉同步组织信息
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        bool SyncFromDingtalk(string appKey, string appSecret);
        #endregion

        List<OrgPost> GetOrgPostsByRoleCode(string roleCode);

        /// <summary>
        /// 获取组织单元信息
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns></returns>
        OrgUnit GetOrgUnit(string objectId);

        /// <summary>
        /// 更新组织单元对象
        /// </summary>
        /// <param name="orgUnit"></param>
        /// <returns></returns>
        bool UpdateOrgUnit(OrgUnit orgUnit);
    }
}
