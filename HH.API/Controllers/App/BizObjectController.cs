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
    [Route("api/[controller]")]
    public class AppController : APIController, IAppController
    {
        public AppController()
        {
            this.appCatalogRepository = this.GetRepository<IAppCatalogRepository>();
            this.appPackageRepository = this.GetRepository<IAppPackageRepository>();
        }

        private IAppPackageRepository appPackageRepository = null;
        private IAppCatalogRepository appCatalogRepository = null;

        [HttpPost("AddAppPackage")]
        public JsonResult AddAppPackage([FromBody] AppPackage appPackage)
        {
            dynamic result = this.appPackageRepository.Insert(appPackage);
            return Json(result);
        }

        [HttpGet("RemoveAppPackage")]
        public JsonResult RemoveAppPackage(string objectId)
        {
            // TODO:验证是否有节点或者数据
            dynamic result = this.appPackageRepository.RemoveObjectById(objectId);
            return Json(result);
        }

        [HttpPost("AddAppCatalog")]
        public JsonResult AddAppCatalog([FromBody] AppCatalog appCatalog)
        {
            // TODO:做验证
            dynamic result = this.appCatalogRepository.Insert(appCatalog);
            return Json(result);
        }

        [HttpGet("RemoveAppCatalog")]
        public JsonResult RemoveAppCatalog(string objectId)
        {
            // TODO:做验证
            bool result = this.appCatalogRepository.RemoveObjectById(objectId);
            return Json(result);
        }

        [HttpPost("UpdateAppCatalog")]
        public JsonResult UpdateAppCatalog([FromBody] AppCatalog appCatalog)
        {
            // TODO:做验证
            dynamic result = this.appCatalogRepository.Update(appCatalog);
            return Json(result);
        }
    }
}
