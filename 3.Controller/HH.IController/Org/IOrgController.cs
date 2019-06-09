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
        /// <summary>
        /// 获取跟组织对象
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRootUnit")]
        JsonResult GetRootUnit();

        /// <summary>
        /// 添加组织单元
        /// </summary>
        /// <param name="orgUnit">组织单元</param>
        /// <returns>返回添加是否成功</returns>
        [HttpPost("AddOrgUnit")]
        JsonResult AddOrgUnit([FromBody]OrgDepartment orgUnit);

        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet("orgUnit")]
        JsonResult RemoveOrgUnit(string userId, string objectId);

        /// <summary>
        /// 更新组织信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgUnit"></param>
        /// <returns></returns>
        [HttpPost("orgUnit")]
        JsonResult UpdateOrgUnit([FromBody]string userId, [FromBody]OrgDepartment orgUnit);

        /// <summary>
        /// 根据上级组织Id获取子组织单元
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="organizationType"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetChildUnits(string parentId, UnitType organizationType);

        /// <summary>
        /// 设置组织对象是否被启用
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut]
        JsonResult SetUnitEnabled(dynamic obj);

        /// <summary>
        /// 获取组织单元信息
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetOrgUnit(string objectId);
        #endregion

        #region 用户信息 -----------------
        /// <summary>
        /// 用户密码重置
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut]
        JsonResult ResetPassword(dynamic obj);

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
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        JsonResult UpdateUser([FromBody]string userId, [FromBody]OrgUser user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpDelete]
        JsonResult RemoveUser(string userId, string objectId);

        /// <summary>
        /// 根据上级组织ID获取用户
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="recurse"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetChildUsers(string parentId, bool recurse);
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
        /// <param name="userId"></param>
        /// <param name="orgRole"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult UpdateOrgRole([FromBody]string userId, [FromBody]OrgRole orgRole);

        /// <summary>
        /// 删除组织角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpDelete]
        JsonResult RemoveOrgRole(string userId, string objectId);
        #endregion

        #region 找人函数 -----------------
        /// <summary>
        /// 根据角色找人
        /// </summary>
        /// <param name="startOrgId">组织Id</param>
        /// <param name="roleCode">角色编码</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult FindUsersByRoleCode(string startOrgId, string roleCode);

        /// <summary>
        /// 查找指定组织下的所有角色人员
        /// </summary>
        /// <param name="orgUnitId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult FindRoleUsersByOrg(string orgUnitId, string roleCode);

        /// <summary>
        /// 获取组织对象的管理者
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetManager(string objectId);

        /// <summary>
        /// 获取组织对象所在的组织单元的管理者
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetOUManager(string objectId);

        /// <summary>
        /// 获取某个特定的组织层级的经理
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="unitLevel"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetUnitLevelManager(string objectId, int unitLevel);

        /// <summary>
        /// 获取指定组织往上层级的经理
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="corssLevel"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult GetCrossLevelManager(string objectId, int corssLevel);
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