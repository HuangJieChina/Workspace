using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using HH.API.IServices;
using HH.API.Entity.BizData;
using HH.API.Entity.BizModel;
using HH.API.IController;
using HH.API.Entity.App;

namespace HH.API.Controllers
{
    /// <summary>
    /// 应用中心
    /// </summary>
    [Route("api/[controller]")]
    public class AppController : APIController, IAppController
    {
        public AppController()
        {
            this.appFunctionRepository = this.GetRepository<IAppFunctionRepository>();
            this.appPackageRepository = this.GetRepository<IAppPackageRepository>();
        }

        private IAppPackageRepository appPackageRepository = null;
        private IAppFunctionRepository appFunctionRepository = null;

        #region 常量定义 -------------------------------
        public const string Method_AddAppPackage = "AddAppPackage";
        public const string Method_RemoveAppPackage = "RemoveAppPackage";
        public const string Method_AddFolder = "AddFolder";
        public const string Method_RemoveFolder = "RemoveFolder";
        #endregion

        /// <summary>
        /// 新增应用包
        /// </summary>
        /// <param name="appPackage"></param>
        /// <returns></returns>
        [HttpPost(Method_AddAppPackage)]
        public JsonResult AddAppPackage([FromBody]AppPackage appPackage)
        {
            // if (string.IsNullOrWhiteSpace(this.SystemCode)) return this.BadRequestJson;
            return this.MonitorFunction<AppPackage>(Method_AddAppPackage, appPackage,
                () =>
                {
                    // 验证编码是否重复
                    if (this.appPackageRepository.GetAppPackageByCode(appPackage.AppCode) != null)
                    {
                        return Json(APIResultCode.CodeDuplicate, "This app code is already exists.");
                    }

                    dynamic result = this.appPackageRepository.Insert(appPackage);
                    return Json(result);
                });
        }

        [HttpGet(Method_RemoveAppPackage)]
        public JsonResult RemoveAppPackage(string objectId)
        {
            // if (string.IsNullOrWhiteSpace(this.CorpId)) return this.BadRequestJson;

            // TODO:验证是否有节点或者数据
            dynamic result = this.appPackageRepository.RemoveObjectById(objectId);
            return Json(result);
        }

        /// <summary>
        /// 添加应用目录
        /// </summary>
        /// <param name="appFunction"></param>
        /// <returns></returns>
        [HttpPost(Method_AddFolder)]
        public JsonResult AddFolder([FromBody]AppFunction appFunction)
        {
            // TODO:通过拦截器实现锁策略
            // 从 Header 中获取 CorpId
            return this.MonitorFunction(Method_AddFolder,
              () =>
              {
                  // 做数据有效性检查
                  JsonResult validateResult = null;
                  if (this.DataValidator<AppFunction>(appFunction, out validateResult)) return validateResult;

                  if (appFunction.ParentId.Equals(appFunction.AppPackageId))
                  {// 根目录，校验应用是否存在 
                      if (this.appPackageRepository.GetObjectById(appFunction.AppPackageId) == null)
                      {
                          return Json(APIResultCode.AppPackageNotExists,
                              string.Format("App package is not exists which package id equals {0}",
                              appFunction.AppPackageId));
                      }
                  }
                  else if (this.appFunctionRepository.GetObjectById(appFunction.ParentId) == null)
                  {
                      // 父级元素不存在
                      return Json(APIResultCode.ParentNotExists, string.Format("Parent is not exists which parent id equals {0}", appFunction.AppPackageId));
                  }
                  // 检查编码是否重复(应用内唯一)
                  if (this.appFunctionRepository.GetFunctionByCode(appFunction.FunctionCode) != null)
                  {
                      return Json(APIResultCode.CodeDuplicate, string.Format("This function code is alerady exists,code={0}", appFunction.FunctionCode));
                  }
                  // 检查名称是否重复
                  if (this.appFunctionRepository.GetFunctionByName(appFunction.ObjectId, appFunction.FunctionName) != null)
                  {
                      return Json(APIResultCode.NameDuplicate, string.Format("The same name exists in the same parent,name={0}", appFunction.FunctionName));
                  }

                  // 新增
                  dynamic res = this.appFunctionRepository.Insert(appFunction);
                  return Json(res);
              });
        }

        [HttpGet(Method_RemoveFolder)]
        public JsonResult RemoveFolder(string objectId)
        {
            throw new NotImplementedException();
        }

        public JsonResult UpdateFolder([FromBody] AppFunction appFunction)
        {
            throw new NotImplementedException();
        }

        public JsonResult AddCustomUrl([FromBody] AppFunction appFunction)
        {
            throw new NotImplementedException();
        }

        public JsonResult SetFunctionPosition(string objectId, int sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
