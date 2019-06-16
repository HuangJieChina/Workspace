using HH.API.Entity;
using HH.API.Entity.Orgainzation;
using HH.API.IRepository;
using HH.API.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Service.Organization
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
        protected IOrgDepartmentRepository orgDepartmentRepository = null;

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
        public bool AddOrgDepartment(string userId, OrgDepartment department)
        {
            this.orgDepartmentRepository.Insert(department);
            // 先做数据格式校验
            this.DataValidator<OrgDepartment>(department);
            // 检查ParentId是否存在
            if (this.orgDepartmentRepository.GetObjectById(department.ParentId) == null)
            {
                throw new APIException(APIResultCode.ParentNotExists,
                   string.Format("组织单元父节点'{0}'不存在", department.ParentId));
            }
            department.IsRootUnit = false;

            dynamic result = this.orgDepartmentRepository.Insert(department);
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
            if (this.orgDepartmentRepository.GetObjectById(user.ParentId) == null)
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

        /// <summary>
        /// 查找当前组织下指定角色的所有成员(不限定管理范围)
        /// </summary>
        /// <param name="orgUnitId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public List<OrgUser> FindRoleUsersByOrg(string orgUnitId, string roleCode)
        {
            List<OrgPost> orgPosts = this.GetOrgPostsByRoleCode(roleCode);
            if (orgPosts == null) return null;

            // 查找组织在orgUnitId范围内的岗位信息
            List<OrgPost> posts = orgPosts.FindAll((orgPost) =>
            {
                return this.orgDepartmentRepository.IsParentDepartment(orgUnitId, orgPost.ParentId);
            });
            if (posts == null) return null;

            List<OrgUser> users = new List<OrgUser>();
            List<string> userIds = new List<string>();
            posts.ForEach((post) =>
            {
                List<OrgUser> orgUsers = this.GetOrgPost(post.ObjectId).ChildUsers;

                orgUsers.ForEach((orgUser) =>
                {
                    if (!userIds.Contains(orgUser.ObjectId))
                    {
                        users.Add(orgUser);
                    }
                });
            });
            return users;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public OrgPost GetOrgPost(string objectId)
        {
            OrgPost post = this.orgPostRepository.GetObjectById(objectId);
            if (post == null) return null;

            if (post.ChildUsers == null)
            {
                // 获取到岗位用户
                List<OrgPostUser> orgPostUsers = this.orgPostUserRepository.GetListByKey(OrgPostUser.PropertyName_PostId, post.ObjectId);
                List<OrgUser> orgUsers = new List<OrgUser>();

                if (orgPostUsers != null)
                {
                    orgPostUsers.ForEach((postUser) =>
                    {
                        OrgUser user = this.orgUserRepository.GetObjectById(postUser.UserId);

                        if (user != null)
                        {
                            orgUsers.Add(user);
                        }
                    });
                }
                // 将岗位用户更新到岗位的缓存
                post.ChildUsers = orgUsers;
                // 更新岗位用户到缓存中
                this.orgPostRepository.SetChildUsers(post);
            }

            return post;
        }

        /// <summary>
        /// 根据角色编码查找管理范围包含startOrgId的岗位的用户集合
        /// </summary>
        /// <param name="startOrgId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public List<OrgUser> FindUsersByRoleCode(string startOrgId, string roleCode)
        {
            List<OrgPost> orgPosts = this.GetOrgPostsByRoleCode(roleCode);
            if (orgPosts == null) return null;

            // 查找管理范围包含startOrgId的岗位
            List<OrgPost> posts = orgPosts.FindAll((orgPost) =>
            {
                return this.orgDepartmentRepository.UnitScopesContains(orgPost.UnitScopes, startOrgId);
            });
            if (posts == null) return null;

            List<OrgUser> users = new List<OrgUser>();
            List<string> userIds = new List<string>();
            posts.ForEach((post) =>
            {
                List<OrgUser> orgUsers = this.GetOrgPost(post.ObjectId).ChildUsers;

                orgUsers.ForEach((orgUser) =>
                {
                    if (!userIds.Contains(orgUser.ObjectId))
                    {
                        users.Add(orgUser);
                    }
                });
            });
            return users;
        }

        /// <summary>
        /// 获取组织子对象集合
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="organizationType"></param>
        /// <returns></returns>
        public List<OrgUnit> GetChildUnits(string parentId, UnitType organizationType)
        {
            List<OrgDepartment> childDepartments = this.orgDepartmentRepository.GetChildDepartmentsByParent(parentId, false);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据组织Id获取所属的用户集合 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="recurse"></param>
        /// <returns></returns>
        public List<OrgUser> GetChildUsers(string parentId, bool recursive)
        {
            return this.orgUserRepository.GetChildUsersByParent(parentId, recursive);
        }

        /// <summary>
        /// 获取组织跨级的经理
        /// </summary>
        /// <para>1:表示当前层级的部门经理,2:表示是上一部门的经理,依次类推</para>
        /// <param name="objectId"></param>
        /// <param name="corssLevel"></param>
        /// <returns></returns>
        public string GetCrossLevelManager(string objectId, int corssLevel)
        {
            if (corssLevel < 1 || corssLevel > 20)
            {
                throw new APIException(APIResultCode.BadParameter,
                    string.Format("Corss level mast between 1 and 20", objectId));
            }

            OrgDepartment department = this.GetOrgDepartmentByUnitId(objectId);
            if (department == null) return null;

            for (int i = 1; i < corssLevel; i++)
            {
                // 获取上一级部门
                department = this.GetOrgDepartmentByUnitId(department.ParentId);
                if (department == null) return null;
                else if (department.IsRootUnit) break;
            }
            return department.ManagerId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public OrgDepartment GetOrgDepartmentByUnitId(string objectId)
        {
            OrgUnit orgUnit = this.GetOrgUnit(objectId);
            if (orgUnit == null) throw new APIException(APIResultCode.UnitIdNotExists,
                string.Format("组织Id'{0}'不存在", objectId));
            if (orgUnit.UnitType.Equals(UnitType.OrgDepartment)) return orgUnit as OrgDepartment;

            return this.GetOrgUnit(orgUnit.ParentId) as OrgDepartment;
        }

        /// <summary>
        /// 根据Id获取组织对象
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public OrgUnit GetOrgUnit(string objectId)
        {
            // 用户
            OrgUnit orgUnit = this.orgUserRepository.GetObjectById(objectId) as OrgUnit;
            if (orgUnit != null) return orgUnit;

            // 部门
            orgUnit = this.orgDepartmentRepository.GetObjectById(objectId) as OrgUnit;
            if (orgUnit != null) return orgUnit;

            // 岗位
            orgUnit = this.orgPostRepository.GetObjectById(objectId) as OrgUnit;
            return orgUnit;
        }

        public bool UpdateOrgUnit(OrgUnit orgUnit)
        {
            switch (orgUnit.UnitType)
            {
                case UnitType.OrgDepartment:
                    this.orgDepartmentRepository.Update((OrgDepartment)orgUnit);
                    break;
                case UnitType.OrgUser:
                    this.orgUserRepository.Update((OrgUser)orgUnit);
                    break;
                case UnitType.OrgPost:
                    this.orgPostRepository.Update((OrgPost)orgUnit);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return true;
        }

        /// <summary>
        /// 获取组织对象的经理
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public string GetManager(string objectId)
        {
            OrgUnit orgUnit = this.GetOrgUnit(objectId);
            if (orgUnit == null) return null;
            return orgUnit.ManagerId;
        }

        public List<OrgPost> GetOrgPostsByRoleCode(string roleCode)
        {
            return this.orgPostRepository.GetOrgPostsByRoleCode(roleCode);
        }

        public string GetOUManager(string objectId)
        {
            OrgDepartment orgDepartment = this.GetOrgDepartmentByUnitId(objectId);
            if (orgDepartment == null) return null;
            return orgDepartment.ManagerId;
        }

        public OrgDepartment GetRootDepartment()
        {
            return this.orgDepartmentRepository.RootDepartment;
        }

        /// <summary>
        /// 获取指定组织层级的经理
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="unitLevel">0:根目录(公司),1:一级部门</param>
        /// <returns></returns>
        public string GetUnitLevelManager(string objectId, int unitLevel)
        {
            OrgUnit orgUnit = this.GetOrgUnit(objectId);
            if (orgUnit == null) return null;

            List<string> parentIds = this.orgDepartmentRepository.GetParentDepartmentIds(orgUnit.ParentId);
            if (parentIds == null) return null;

            if (parentIds.Count <= unitLevel) return null;
            return parentIds[unitLevel];
        }

        /// <summary>
        /// 删除指定的组织角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public bool RemoveOrgRoleByCode(string userId, string roleCode)
        {
            OrgRole orgRole = this.orgRoleRepository.GetOrgRoleByCode(roleCode);
            if (orgRole == null) return true;

            List<OrgPost> orgPosts = this.orgPostRepository.GetOrgPostsByRoleCode(roleCode);
            if (orgPosts != null && orgPosts.Count > 0)
                throw new APIException(APIResultCode.RoleCannotRemove, "该角色已被绑定岗位，请先删除该角色的岗位再删除角色!");

            // 再删除所有的角色
            this.orgRoleRepository.RemoveObjectById(orgRole.ObjectId);

            return true;
        }

        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public bool RemoveOrgDepartment(string userId, string objectId)
        { // 如果组织机构下有岗位或者用户，则不允许被删除
            List<OrgUnit> childs = this.GetChildUnits(objectId, UnitType.AllType);
            if (childs != null && childs.Count > 0)
            {
                throw new APIException(APIResultCode.ExistsChildren, "存在子对象,不允许被删除!");
            }
            return this.orgDepartmentRepository.RemoveObjectById(objectId);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public bool RemoveUser(string userId, string objectId)
        {
            // 删除用户前需要检测任务是否完成，因为跨服务，这个逻辑需要放在调用方
            this.orgUserRepository.RemoveObjectById(objectId);
            // 检查用户是否有Manager绑定当前用户;
            List<dynamic> userIds = this.orgUserRepository.QueryUserByManagerId(objectId);
            if (userIds != null)
            {
                userIds.ForEach((id) =>
                {
                    OrgUser user = this.orgUserRepository.GetObjectById(id);
                    if (user != null)
                    {
                        user.ManagerId = null;
                        this.orgUserRepository.Update(user);
                    }
                });
            }
            // 检查组织是否有Manager绑定当前用户
            List<dynamic> departmentIds = this.orgDepartmentRepository.QueryDepartmentByManagerId(objectId);
            if (userIds != null)
            {
                userIds.ForEach((id) =>
                {
                    OrgDepartment department = this.orgDepartmentRepository.GetObjectById(id);
                    if (department != null)
                    {
                        department.ManagerId = null;
                        this.orgDepartmentRepository.Update(department);
                    }
                });
            }
            return true;
        }

        /// <summary>
        /// 用户密码验证
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidPassword(string userCode, string password)
        {
            OrgUser user = this.orgUserRepository.GetOrgUserByCode(userCode);
            if (user == null) return false;

            // 密码验证
            bool res = user.ValidatePassword(password);
            if (!res)
            {
                if (user.PasswordInvalids > 5)
                {// 大于5次，锁定该用户，管理员重置密码则自动解锁
                    user.IsLocked = true;
                }
                this.orgUserRepository.Update(user);
            }
            return res;
        }

        /// <summary>
        /// 重设用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(string userId, string objectId, string newPassword)
        {
            OrgUser user = this.orgUserRepository.GetObjectById(objectId);
            user.SetPassword(newPassword);
            this.orgUserRepository.Update(user);

            return true;
        }

        /// <summary>
        /// 设置组织单位被禁用
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="objectId"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public bool SetUnitEnabled(string userId, string objectId, bool enabled)
        {
            OrgUnit orgUnit = this.GetOrgUnit(objectId);
            if (orgUnit == null) return false;

            if (!enabled && orgUnit.UnitType == UnitType.OrgDepartment)
            {// 部门的禁用，需要里面所有的内容都是禁用的
                List<OrgUnit> orgUnits = this.GetChildUnits(objectId, UnitType.AllType);
                if (orgUnits != null && orgUnits.Find((unit) => unit.IsEnabled) != null)
                {
                    throw new APIException(APIResultCode.UnitCannotDisabled, "存在不是被禁用的子组织，不允许被禁用!");
                }
            }
            else if (enabled)
            {// 如果是被启用，则需要检查上级组织是否是启用状态，否则不允许被启用
                OrgDepartment department = this.GetOrgDepartmentByUnitId(orgUnit.ParentId);
                if (department != null && !department.IsEnabled)
                {
                    throw new APIException(APIResultCode.UnitCannotEnabled, "请先启用上级组织部门，再启用该组织对象!");
                }
            }
            orgUnit.IsEnabled = enabled;
            this.UpdateOrgUnit(orgUnit);
            return true;
        }

        public bool UpdateOrgRole(string userId, OrgRole orgRole)
        {
            return this.orgRoleRepository.Update(orgRole);
        }

        public bool UpdateOrgDepartment(string userId, OrgDepartment department)
        {
            return this.orgDepartmentRepository.Update(department);
        }

        public bool UpdateUser(string userId, OrgUser user)
        {
            return this.orgUserRepository.Update(user);
        }

        public bool IsParentUnit(string parentId, string childId)
        {
            return this.orgDepartmentRepository.IsParentDepartment(parentId, childId);
        }

        /// <summary>
        /// 从钉钉同步组织信息(完整同步)
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public bool SyncFromDingtalk(string appKey, string appSecret)
        {
            SyncOrgFromDingtalk syncFromDingtalk = new SyncOrgFromDingtalk(appKey, appSecret);
            return syncFromDingtalk.Sync();
        }
    }
}