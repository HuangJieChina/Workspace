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
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet]
        ActionResult RemoveUnit(string objectId);

        /// <summary>
        /// 更新组织信息
        /// </summary>
        /// <param name="orgUnit">组织对象</param>
        [HttpPost]
        ActionResult UpdateUnit([FromBody]OrgUnit orgUnit);
        #endregion
    }
}
