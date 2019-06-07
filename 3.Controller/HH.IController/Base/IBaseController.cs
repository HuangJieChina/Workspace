using HH.API.Entity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HH.API.IController
{
    /// <summary>
    /// 接口基类
    /// 注：所有的接口参数均以对象方式传递，如果没有实体对象，那么一 dynamic 传递
    /// </summary>
    public interface IBaseController
    {
        /// <summary>
        /// 获取服务器的时间
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSystemDateTime")]
        JsonResult GetSystemDateTime();
    }
}