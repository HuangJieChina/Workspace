using HH.API.Entity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HH.API.IController
{
    /// <summary>
    /// 组织机构服务接口类
    /// </summary>
    public interface IOrgController : IBaseController
    {
        #region 组织机构 -----------------
        /// <summary>
        /// 添加组织信息
        /// </summary>
        /// <param name="orgUnit">组织对象</param>
        [HttpPost]
        JsonResult AddUnit([FromBody]OrgUnit orgUnit);

        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns>返回结果</returns>
        [HttpGet]
        JsonResult RemoveUnit(string objectId);

        /// <summary>
        /// 更新组织信息
        /// </summary>
        /// <param name="orgUnit">组织对象</param>
        /// <returns></returns>
        [HttpPost]
        JsonResult UpdateUnit([FromBody]OrgUnit orgUnit);

        /// <summary>
        /// 根据上级组织ID获取子组织单元
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetChildUnitsByParent(string parentId);

        /// <summary>
        /// 获取组织单元信息
        /// </summary>
        /// <param name="objectId">组织ID</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetOrgUnit(string objectId);
        #endregion

        #region 用户信息 -----------------
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddUser([FromBody]OrgUser user);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        JsonResult UpdateUser([FromBody]OrgUser user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="objectId">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult RemoveUser(string objectId);

        /// <summary>
        /// 根据上级组织ID获取用户
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetChildUsersByParent(string parentId);
        #endregion

        #region  角色信息 -----------------
        /// <summary>
        /// 增加组织角色
        /// </summary>
        /// <param name="orgRole"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddOrgRole([FromBody]OrgRole orgRole);

        /// <summary>
        /// 更新组织角色
        /// </summary>
        /// <param name="orgRole"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult UpdateOrgRole([FromBody]OrgRole orgRole);

        /// <summary>
        ///  删除组织角色
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult RemoveOrgRole(string objectId);
        #endregion

        #region 找人函数 -----------------
        /// <summary>
        /// 根据角色找人
        /// </summary>
        /// <param name="orgId">组织Id</param>
        /// <param name="roleCode">角色编码</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult FindRoleUsers(string orgId, string roleCode);

        /// <summary>
        /// 获取组织对象的管理者
        /// </summary>
        /// <param name="orgId">组织Id</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetManager(string orgId);
        #endregion
    }
}
