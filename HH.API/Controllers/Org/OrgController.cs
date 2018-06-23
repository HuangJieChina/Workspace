using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using HH.API.IController;
using HH.API.IServices;

namespace HH.API.Controllers
{
    /// <summary>
    /// 组织服务类
    /// </summary>
    [Route("api/[controller]")]
    public class OrgController : APIController, IOrgController
    {
        #region 依赖注入的服务对象 ---------------
        public IOrgUnitRepository orgUnitRepository = null;
        public IOrgUserRepository orgUserRepository = null;
        public IOrgRoleRepository orgRoleRepository = null;
        public IOrgRoleUserRepository orgRoleUserRepository = null;
        #endregion

        public OrgController(IOrgUnitRepository orgUnitRepository,
            IOrgUserRepository orgUserRepository,
            IOrgRoleRepository orgRoleRepository,
            IOrgRoleUserRepository orgRoleUserRepository)
        {
            this.orgUnitRepository = orgUnitRepository;
            this.orgUserRepository = orgUserRepository;
            this.orgRoleRepository = orgRoleRepository;
            this.orgRoleUserRepository = orgRoleUserRepository;
        }

        public JsonResult AddOrgRole([FromBody] OrgRole orgRole)
        {
            this.orgRoleRepository.Insert(orgRole);
            JsonResult validateResult = null;
            if (this.DataValidator<OrgRole>(orgRole, out validateResult)) return validateResult;

            if (this.orgRoleRepository.GetOrgRoleByCode(orgRole.Code) != null)
            {
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.CodeDuplicate,
                    Message = "角色编码已经存在"
                });
            }

            dynamic result = this.orgRoleRepository.Insert(orgRole);
            return Json(new APIResult() { ResultCode = ResultCode.Success, Extend = orgRole });
        }

        /// <summary>
        /// 新增组织单元
        /// </summary>
        /// <param name="orgUnit"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddUnit([FromBody] OrgUnit orgUnit)
        {
            #region 数据格式校验 -----------------
            // 数据格式校验
            JsonResult validateResult = null;
            if (this.DataValidator<OrgUnit>(orgUnit, out validateResult)) return validateResult;

            // 验证上级组织是否存在
            OrgUnit parentUnit = this.orgUnitRepository.GetObjectById(orgUnit.ObjectId);
            if (parentUnit == null)
            {
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.ParentNotExists,
                    Message = "上级组织不存在"
                });
            }
            //  验证同一组织下是否已经存在同名
            if (this.orgUnitRepository.IsExistsOrgName(orgUnit.ParentId, orgUnit.UnitName))
            {
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.ParentNotExists,
                    Message = "上级组织已经存在相同名称组织"
                });
            }
            #endregion

            // 强制设置为非根节点
            orgUnit.IsRootUnit = false;

            dynamic result = this.orgUnitRepository.Insert(orgUnit);
            return Json(new APIResult() { ResultCode = ResultCode.Success, Extend = orgUnit });
        }

        [HttpPost]
        public JsonResult AddUser([FromBody]OrgUser user)
        {
            #region 数据格式校验 ---------------
            // 先做数据格式校验
            JsonResult validateResult = null;
            if (this.DataValidator<OrgUser>(user, out validateResult)) return validateResult;

            // 验证上级组织是否存在
            OrgUnit parentUnit = this.orgUnitRepository.GetObjectById(user.ParentId);
            if (parentUnit == null)
            {
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.ParentNotExists,
                    Message = "上级组织不存在"
                });
            }
            //  验证编码是否重复
            if (this.orgUserRepository.GetOrgUserByCode(user.Code) != null)
            {
                return Json(new APIResult()
                {
                    ResultCode = ResultCode.CodeDuplicate,
                    Message = "用户编码已经存在"
                });
            }
            #endregion
            dynamic result = this.orgUserRepository.Insert(user);
            return Json(new APIResult() { ResultCode = ResultCode.Success, Extend = user });
        }

        public JsonResult FindRoleUsers(string orgId, string roleCode)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public JsonResult GetChildUnitsByParent(string parentId)
        {
            List<OrgUnit> orgUnits = this.orgUnitRepository.GetChildUnitsByParent(parentId, false);
            return Json(orgUnits);
        }

        [HttpGet]
        public JsonResult GetChildUsersByParent(string parentId)
        {
            List<OrgUser> orgUsers = this.orgUserRepository.GetChildUsersByParent(parentId, false);
            return Json(orgUsers);
        }

        public JsonResult GetManager(string orgId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public JsonResult GetOrgUnit(string objectId)
        {
            return Json(this.orgUnitRepository.GetObjectById(objectId));
        }

        public JsonResult RemoveOrgRole(string objectId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public JsonResult RemoveUnit(string objectId)
        {
            bool res = this.orgUnitRepository.RemoveObjectById(objectId);
            return Json(res);
        }

        [HttpGet]
        public JsonResult RemoveUser(string objectId)
        {
            bool res = this.orgUserRepository.RemoveObjectById(objectId);
            return Json(res);
        }

        public JsonResult UpdateOrgRole([FromBody] OrgRole orgRole)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public JsonResult UpdateUnit([FromBody] OrgUnit orgUnit)
        {
            bool res = this.orgUnitRepository.Update(orgUnit);
            return Json(res);
        }

        [HttpPost]
        public JsonResult UpdateUser([FromBody] OrgUser user)
        {
            bool res = this.orgUserRepository.Update(user);
            return Json(res);
        }
    }
}
