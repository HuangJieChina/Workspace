using HH.API.Entity;
using HH.API.Entity.App;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.IController
{
    /// <summary>
    /// App应用接口
    /// </summary>
    public interface IAppController : IBaseController
    {
        /// <summary>
        /// 新增App应用
        /// </summary>
        /// <param name="appPackage"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddAppPackage([FromBody]AppPackage appPackage);

        /// <summary>
        /// 删除App应用
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult RemoveAppPackage(string objectId);

        /// <summary>
        /// 增加应用目录
        /// </summary>
        /// <param name="appCatalog"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddAppCatalog([FromBody] AppCatalog appCatalog);

        /// <summary>
        /// 删除App应用目录
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult RemoveAppCatalog(string objectId);

        /// <summary>
        /// 更新应用目录
        /// </summary>
        /// <param name="appCatalog"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult UpdateAppCatalog([FromBody] AppCatalog appCatalog);

    }
}