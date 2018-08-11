using HH.API.Entity;
using HH.API.Entity.Orgainzation;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HH.API.IController
{
    /// <summary>
    /// 组织机构服务接口类(基础属性：组织、用户，业务属性：角色/岗位)
    /// </summary>
    public interface IOrgController : IBaseController
    {
        #region 组织机构 -----------------
        [HttpGet("GetRootUnit")]
        JsonResult GetRootUnit();

        /// <summary>
        /// 添加组织信息
        /// </summary>
        /// <param name="orgUnit">组织对象</param>
        [HttpPost]
        JsonResult AddOrgUnit([FromBody]OrgUnit orgUnit);

        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns>返回结果</returns>
        [HttpGet]
        JsonResult RemoveOrgUnit(string objectId);

        /// <summary>
        /// 更新组织信息
        /// </summary>
        /// <param name="orgUnit">组织对象</param>
        /// <returns></returns>
        [HttpPost]
        JsonResult UpdateOrgUnit([FromBody]OrgUnit orgUnit);

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
        /// 用户密码重置
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult ResetPassword(string userCode, string oldPassword, string newPassword);

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
        /// <param name="objectId">组织Id</param>
        /// <param name="roleCode">角色编码</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult FindRoleUsersByCode(string objectId, string roleCode);

        /// <summary>
        /// 查找指定组织下的所有角色人员
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult FindRoleUsersByOrg(string objectId, string roleCode);

        /// <summary>
        /// 获取组织对象的管理者
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetManager(string objectId);
        #endregion

        /*
         找人函数：
         1.查找上级经理
         2.查找组织经理
         3.递归上级到组织经理
         4.以组织为起始点查找指定角色
         5.查找指定组织下的某个角色的所有成员
         6.查找上级角色
         7.绑定指定岗位
         */
    }
}