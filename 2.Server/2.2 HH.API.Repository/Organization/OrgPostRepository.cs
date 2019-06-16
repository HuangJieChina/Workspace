using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IRepository;
using HH.API.Entity.Orgainzation;
using HH.API.Entity.Cache.KeyCollectionCache;

namespace HH.API.Repository
{
    public class OrgPostRepository : RepositoryBase<OrgPost>, IOrgPostRepository
    {
        public OrgPostRepository() : base()
        {

        }

        private IKeyCollectionCache<OrgPost> _RoleCodePosts = null;

        protected IKeyCollectionCache<OrgPost> RoleCodePosts
        {
            get
            {
                if (_RoleCodePosts == null)
                {
                    _RoleCodePosts = KeyCollectionCacheFactory<OrgPost>.Instance.GetCache(OrgPost.PropertyName_RoleCode);
                }
                return _RoleCodePosts;
            }
        }


        ///// <summary>
        ///// 根据角色编码查找所有岗位
        ///// </summary>
        ///// <param name="startOrgId"></param>
        ///// <param name="roleCode"></param>
        ///// <returns></returns>
        //public List<string> FindRoleUserIds(string startOrgId, string roleCode)
        //{
        //    List<string> userIds = new List<string>();
        //    this.FindRoleUsers(startOrgId, roleCode).ForEach((user) => { userIds.Add(user.ObjectId); });
        //    return userIds;
        //}

        //public List<OrgUser> FindRoleUsers(string startOrgId, string roleCode)
        //{
        //    List<OrgPost> orgPosts = this.GetOrgPostsByRoleCode(roleCode);
        //    if (orgPosts == null) return null;

        //    orgPosts.ForEach((orgPost) =>
        //    {
        //        // TODO：判断某个组织是否另外一个组织的父组织
        //    });
        //    return new List<OrgUser>();
        //}

        public List<OrgPost> GetOrgPostsByRoleCode(string roleCode)
        {
            if (!this.RoleCodePosts.ContainsKey(roleCode))
            {// 先获取所有角色对应的岗位，进入缓存
                List<OrgPost> orgPosts = this.GetListByKey(OrgPost.PropertyName_RoleCode, roleCode);

                if (orgPosts != null)
                {
                    orgPosts.ForEach((orgPost) =>
                    {// 刷新岗位缓存信息
                        this.RefreshEntityToCache(orgPost);
                    });
                }
                this.RoleCodePosts.Save(roleCode, orgPosts);
            }
            return this.RoleCodePosts.Get(roleCode);
        }

        public void SetChildUsers(OrgPost orgPost)
        {
            this.SaveEntityToCache(orgPost);
            // this.RefreshEntityToCache(orgPost);
        }

        public bool RemoveOrgPostsByRoleCode(string roleCode)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// 获取岗位包含的用户集合
        ///// </summary>
        ///// <param name="objectId"></param>
        ///// <returns></returns>
        //public List<OrgUser> GetChildUsers(string objectId)
        //{
        //    //OrgPost orgPost = this.GetObjectById(objectId);
        //    //if (orgPost.ChildUsers != null) return orgPost.ChildUsers;
        //}
    }
}