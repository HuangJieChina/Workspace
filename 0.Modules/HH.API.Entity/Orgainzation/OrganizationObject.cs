using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HH.API.Entity.Orgainzation
{
    /// <summary>
    /// 组织对象基类
    /// </summary>
    [Serializable]
    public class OrganizationObject : EntityBase
    {
        /// <summary>
        /// 获取或设置用户的上级经理
        /// </summary>
        [StringLength(36)]
        [Column(TypeName = "char")]
        public string ManagerId { get; set; }

        /// <summary>
        /// 获取或设置中文显示名称
        /// </summary>
        [Required(ErrorMessage = "中文显示名称不允许为空")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置英文显示名称
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 获取或设置用户所属组织Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 获取或设置当前对象是否被启用
        /// </summary>
        /// <para>true:启用,false:禁用</para>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 获取或设置同步源的组织对象Id
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 获取或设置在上级组织中的排序值
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// 获取或设置存储表名称
        /// </summary>
        public override string TableName => throw new NotImplementedException();

        /// <summary>
        /// 获取组织对象类型
        /// </summary>
        public virtual OrganizationType OrganizationType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }

    /// <summary>
    /// 获取组织机构对象类型
    /// </summary>
    public enum OrganizationType
    {
        /// <summary>
        /// 所有类型
        /// </summary>
        AllType = 0,
        /// <summary>
        /// 组织单元
        /// </summary>
        OrgUnit = 1,
        /// <summary>
        /// 用户
        /// </summary>
        OrgUser = 2,
        /// <summary>
        /// 岗位
        /// </summary>
        OrgPost = 3
    }
}