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
        public string ManagerId { get; set; }

        /// <summary>
        /// 获取或设置用户所属组织Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 获取或设置用户是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 获取或设置同步源的用户ID
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 获取或设置存储表名称
        /// </summary>
        public override string TableName => throw new NotImplementedException();
    }
}
