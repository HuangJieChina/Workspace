using HH.API.Entity;
using HH.API.Entity.Orgainzation;
using HH.API.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Services.Org
{
    /// <summary>
    /// 承载所有的组织服务方法
    /// </summary>
    public class OrganizationService : ServiceBase, IOrganizationService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrganizationService()
        {
        }

        protected IOrgPostRepository orgPostRepository = null;
        protected IOrgPostUserRepository orgPostUserRepository = null;
        protected IOrgRoleRepository orgRoleRepository = null;
        protected IOrgUserRepository orgUserRepository = null;
        protected IOrgUnitRepository orgUnitRepository = null;

        /// <summary>
        /// 增加系统角色
        /// </summary>
        /// <param name="orgRole"></param>
        /// <returns></returns>
        public bool AddOrgRole(string userId, OrgRole orgRole)
        {
            this.orgRoleRepository.Insert(orgRole);
            // 先做数据格式校验
            this.DataValidator<OrgRole>(orgRole);

            if (this.orgRoleRepository.GetOrgRoleByCode(orgRole.Code) != null)
            {
                throw new APIException(APIResultCode.CodeDuplicate,
                    string.Format("角色编码'{0}'违反唯一性原则", orgRole.Code));
            }

            dynamic result = this.orgRoleRepository.Insert(orgRole);
            return true;
        }

        /// <summary>
        /// 添加组织单元
        /// </summary>
        /// <param name="orgUnit"></param>
        /// <returns></returns>
        public bool AddOrgUnit(string userId, OrgUnit orgUnit)
        {
            this.orgUnitRepository.Insert(orgUnit);
            // 先做数据格式校验
            this.DataValidator<OrgUnit>(orgUnit);
            // 检查ParentId是否存在
            if (this.orgUnitRepository.GetObjectById(orgUnit.ParentId) == null)
            {
                throw new APIException(APIResultCode.ParentNotExists,
                   string.Format("组织单元父节点'{0}'不存在", orgUnit.ParentId));
            }

            dynamic result = this.orgUnitRepository.Insert(orgUnit);
            return true;
        }

        public bool AddUser(string userId, OrgUser user)
        {
            // 先做数据格式校验
            this.DataValidator<OrgUser>(user);

            if (this.orgUserRepository.GetOrgUserByCode(user.Code) != null)
            {
                throw new APIException(APIResultCode.CodeDuplicate,
                    string.Format("用户编码'{0}'违反唯一性原则", user.Code));
            }
            // 检查ParentId是否存在
            if (this.orgUnitRepository.GetObjectById(user.ParentId) == null)
            {
                throw new APIException(APIResultCode.ParentNotExists,
                   string.Format("组织父节点'{0}'不存在", user.ParentId));
            }

            // TODO:默认密码
            string defaultPassword = "123456";
            // 获取到用户的默认密码
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.SetPassword(defaultPassword);
            }

            dynamic result = this.orgUserRepository.Insert(user);

            return true;
        }

        public List<OrgUser> FindRoleUsersByOrg(string orgUnitId, string roleCode)
        {
            throw new NotImplementedException();
        }

        public List<OrgUser> FindUsersByRoleCode(string startOrgId, string roleCode)
        {
            throw new NotImplementedException();
        }

        public List<OrganizationObject> GetChildUnits(string parentId, OrganizationType organizationType)
        {
            throw new NotImplementedException();
        }

        public List<OrgUser> GetChildUsers(string parentId, bool recurse)
        {
            throw new NotImplementedException();
        }

        public string GetCrossLevelManager(string objectId, int corssLevel)
        {
            throw new NotImplementedException();
        }

        public string GetManager(string objectId)
        {
            throw new NotImplementedException();
        }

        public List<OrgPost> GetOrgPostsByRoleCode(string roleCode)
        {
            throw new NotImplementedException();
        }

        public OrgUnit GetOrgUnit(string objectId)
        {
            throw new NotImplementedException();
        }

        public string GetOUManager(string objectId)
        {
            throw new NotImplementedException();
        }

        public OrgUnit GetRootUnit()
        {
            throw new NotImplementedException();
        }

        public string GetUnitLevelManager(string objectId, int unitLevel)
        {
            throw new NotImplementedException();
        }

        public bool RemoveOrgRole(string userId, string objectId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveOrgUnit(string userId, string objectId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(string userId, string objectId)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string userId, string objectId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool SetUnitEnabled(string userId, string objectId, bool enabled)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrgRole(string userId, OrgRole orgRole)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrgUnit(string userId, OrgUnit orgUnit)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(string userId, OrgUser user)
        {
            throw new NotImplementedException();
        }
    }
}