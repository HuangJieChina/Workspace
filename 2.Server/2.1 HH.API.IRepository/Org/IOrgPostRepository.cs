﻿using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.Orgainzation;
using System;
using System.Collections.Generic;

namespace HH.API.IRepository
{
    /// <summary>
    /// 岗位
    /// </summary>
    public interface IOrgPostRepository : IRepositoryBase<OrgPost>
    {
        /// <summary>
        /// 根据角色编码查找所有用户Id的集合
        /// </summary>
        /// <param name="startOrgId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        //List<string> FindRoleUserIds(string startOrgId, string roleCode);

        /// <summary>
        /// 根据角色查找所有用户的集合
        /// </summary>
        /// <param name="startOrgId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        ///List<OrgUser> FindRoleUsers(string startOrgId, string roleCode);

        List<OrgPost> GetOrgPostsByRoleCode(string roleCode);

        /// <summary>
        /// 设置岗位包含的用户信息
        /// </summary>
        /// <param name="orgPost"></param>
        void SetChildUsers(OrgPost orgPost);

        /// <summary>
        /// 根据角色编码移除所有的岗位
        /// </summary>
        /// <returns></returns>
        bool RemoveOrgPostsByRoleCode(string roleCode);

        /// <summary>
        /// 获取岗位包含的用户信息
        /// </summary>
        /// <param name="orgPostId"></param>
        /// <returns></returns>
        // List<OrgUser> GetChildUsers(string orgPostId);
    }
}
