using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using HH.API.IController;
using HH.API.IServices;
using HH.API.Entity.Orgainzation;
using HH.API.Authorization;
using HH.API.Services;

namespace HH.API.Controllers
{
    /// <summary>
    /// 组织服务类
    /// </summary>
    [Route("api/[controller]")]
    public class OrgController : APIController, IOrgController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="orgUnitRepository"></param>
        /// <param name="orgUserRepository"></param>
        /// <param name="orgRoleRepository"></param>
        /// <param name="orgRoleUserRepository"></param>
        /// <param name="orgGroupRepository"></param>
        public OrgController(IOrgUnitRepository orgUnitRepository,
            IOrgUserRepository orgUserRepository,
            IOrgRoleRepository orgRoleRepository,
            IOrgPostUserRepository orgPostUserRepository,
            IOrgPostRepository orgPostRepository)
        {
            this.orgUnitRepository = this.GetRepository<IOrgUnitRepository>();
            // this.orgUserRepository = this.GetRepository<IOrgUserRepository>();
            this.orgUserRepository = orgUserRepository;
            this.orgRoleRepository = orgRoleRepository;
            // this.orgGroupRepository = orgGroupRepository;
        }

        #region 依赖注入的服务对象 ---------------
        public IOrgUnitRepository orgUnitRepository = null;
        public IOrgUserRepository orgUserRepository = null;
        public IOrgRoleRepository orgRoleRepository = null;
        public IOrgPostUserRepository orgPostUserRepository = null;
        #endregion

        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="orgRole"></param>
        /// <returns></returns>
        public JsonResult AddOrgRole([FromBody]OrgRole orgRole)
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
        /// 获取组织根节点
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRootUnit")]
        public JsonResult GetRootUnit()
        {
            return Json(this.orgUnitRepository.RootUnit);
        }

        /// <summary>
        /// 新增组织单元
        /// </summary>
        /// <param name="orgUnit"></param>
        /// <returns></returns>
        [HttpPost("AddOrgUnit")]
        public JsonResult AddOrgUnit([FromBody]OrgUnit orgUnit)
        {
            #region 数据格式校验 -----------------
            // 数据格式校验
            JsonResult validateResult = null;
            if (!this.DataValidator<OrgUnit>(orgUnit, out validateResult)) return validateResult;

            // 验证上级组织是否存在
            OrgUnit parentUnit = this.orgUnitRepository.GetObjectById(orgUnit.ParentId);
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

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
            // 设置密码为加密方式
            user.SetPassword(user.Password);

            dynamic result = this.orgUserRepository.Insert(user);
            return Json(new APIResult() { ResultCode = ResultCode.Success, Extend = user });
        }

        /// <summary>
        /// 根据角色查找用户
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        [HttpGet("FindRoleUsers")]
        public JsonResult FindRoleUsers(string objectId, string roleCode)
        {
            if (!this.ValidationOrganization(AuthorizationMode.View, objectId)) return PermissionDenied;

            throw new NotImplementedException();
        }

        [HttpGet("GetChildUnitsByParent")]
        public JsonResult GetChildUnitsByParent(string parentId)
        {
            if (!this.ValidationOrganization(AuthorizationMode.View, parentId)) return PermissionDenied;

            List<OrgUnit> orgUnits = this.orgUnitRepository.GetChildUnitsByParent(parentId, false);
            return Json(orgUnits);
        }

        [HttpGet("GetChildUsersByParent")]
        public JsonResult GetChildUsersByParent(string parentId)
        {
            if (!this.ValidationOrganization(AuthorizationMode.View, parentId)) return PermissionDenied;

            List<OrgUser> orgUsers = this.orgUserRepository.GetChildUsersByParent(parentId, false);
            return Json(orgUsers);
        }

        /// <summary>
        /// 获取经理(用户/组织/用户组)
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet("GetManager")]
        public JsonResult GetManager(string objectId)
        {
            if (!this.ValidationOrganization(AuthorizationMode.View, objectId)) return PermissionDenied;

            OrganizationObject organizationObject = this.GetOrganizationObjectById(objectId);
            OrgUser user = this.orgUserRepository.GetObjectById(organizationObject.ManagerId);
            return Json(user);
        }

        [HttpGet]
        public JsonResult GetOrgUnit(string objectId)
        {
            return Json(this.orgUnitRepository.GetObjectById(objectId));
        }

        [HttpGet("RemoveOrgRole")]
        public JsonResult RemoveOrgRole(string objectId)
        {
            // 权限验证
            if (!this.ValidationOrganization(AuthorizationMode.Admin, objectId)) return PermissionDenied;

            bool res = this.orgRoleRepository.RemoveObjectById(objectId);
            return Json(res);
        }

        [HttpGet]
        public JsonResult RemoveOrgUnit(dynamic orgUnit)
        {
            string objectId = orgUnit.objectId; // 被删除的组织机构 Id
            string userId = orgUnit.userId;     // 当前操作的用户 Id

            bool res = this.orgUnitRepository.RemoveObjectById(objectId);
            return Json(res);
        }

        [HttpGet("RemoveUser")]
        public JsonResult RemoveUser(string objectId)
        {
            bool res = this.orgUserRepository.RemoveObjectById(objectId);
            return Json(res);
        }

        [HttpGet("ResetPassword")]
        public JsonResult ResetPassword(dynamic obj)
        {
            string userCode = obj.userCode;
            string oldPassword = obj.oldPassword;
            string newPassword = obj.newPassword;

            throw new NotImplementedException();
        }

        [HttpPost("UpdateOrgRole")]
        public JsonResult UpdateOrgRole([FromBody]OrgRole orgRole)
        {
            bool res = this.orgRoleRepository.Update(orgRole);
            return Json(res);
        }

        [HttpPost("UpdateOrgUnit")]
        public JsonResult UpdateOrgUnit([FromBody]OrgUnit orgUnit)
        {
            bool res = this.orgUnitRepository.Update(orgUnit);
            return Json(res);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("UpdateUser")]
        public JsonResult UpdateUser([FromBody]OrgUser user)
        {
            // 管理权限、查看权限
            if (!this.ValidationOrganization(AuthorizationMode.Admin, user.ObjectId)) return PermissionDenied;

            bool res = this.orgUserRepository.Update(user);
            return Json(res);
        }

        #region 私有方法 ---------------------------
        /// <summary>
        /// 根据组织Id获取组织对象
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        protected OrganizationObject GetOrganizationObjectById(string objectId)
        {
            OrganizationObject organizationObject = null;
            organizationObject = this.orgUnitRepository.GetObjectById(objectId) as OrganizationObject;
            if (organizationObject != null) return organizationObject;

            organizationObject = this.orgUserRepository.GetObjectById(objectId) as OrganizationObject;
            if (organizationObject != null) return organizationObject;

            return organizationObject;
        }

        public JsonResult FindRoleUsersByCode(string objectId, string roleCode)
        {
            throw new NotImplementedException();
        }

        public JsonResult FindRoleUsersByOrg(string objectId, string roleCode)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
