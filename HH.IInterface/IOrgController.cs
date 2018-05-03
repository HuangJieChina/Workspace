using HH.API.Entity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HH.IInterface
{
    /// <summary>
    /// 组织机构服务接口类
    /// </summary>
    public interface IOrgController
    {
        #region 组织机构 -----------------
        /// <summary>
        /// 添加组织信息
        /// </summary>
        /// <param name="orgUnit">组织对象</param>
        [HttpPost]
        ActionResult AddUnit([FromBody]OrgUnit orgUnit);

        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <param name="objectId">组织Id</param>
        /// <returns>返回结果</returns>
        [HttpGet]
        ActionResult RemoveUnit([FromHeader]string objectId);

        /// <summary>
        /// 更新组织信息
        /// </summary>
        /// <param name="orgUnit">组织对象</param>
        /// <returns></returns>
        [HttpPost]
        ActionResult UpdateUnit([FromBody]OrgUnit orgUnit);

        /// <summary>
        /// 根据上级组织ID获取子组织单元
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        ActionResult GetChildUnitsByParent(string parentId);

        /// <summary>
        /// 获取组织单元信息
        /// </summary>
        /// <param name="objectId">组织ID</param>
        /// <returns></returns>
        [HttpGet]
        OrgUnit GetUnit(string objectId);
        #endregion

        #region 用户信息 -----------------
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        ActionResult AddUser([FromBody]OrgUser user);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        ActionResult UpdateUser([FromBody]OrgUser user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="objectId">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        ActionResult RemoveUser([FromHeader] string objectId);


        /// <summary>
        /// 根据上级组织ID获取用户
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        ActionResult GetChildUsersByParent(string parentId);
        #endregion
    }
}
