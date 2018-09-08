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
        /// <param name="appFunction">目录对象</param>
        /// <returns>返回添加结果</returns>
        [HttpPost]
        JsonResult AddFolder([FromBody]AppFunction appFunction);

        /// <summary>
        /// 删除App应用目录
        /// </summary>
        /// <param name="objectId">目录Id</param>
        /// <returns>返回删除结果</returns>
        [HttpGet]
        JsonResult RemoveFolder(string objectId);

        /// <summary>
        /// 更新应用目录
        /// </summary>
        /// <param name="appFunction"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult UpdateFolder([FromBody]AppFunction appFunction);

        /// <summary>
        /// 新增自定义URL链接
        /// </summary>
        /// <param name="appFunction"></param>
        /// <returns></returns>
        [HttpPost]
        JsonResult AddCustomUrl([FromBody]AppFunction appFunction);

        /// <summary>
        /// 设置应用菜单或目录的排序位置
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [HttpGet]
        JsonResult SetFunctionPosition(string objectId, int sortOrder);

    }
}