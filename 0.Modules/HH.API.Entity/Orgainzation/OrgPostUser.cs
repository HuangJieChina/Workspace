using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.API.Entity.Orgainzation
{
    /// <summary>
    /// 角色用户映射表
    /// </summary>
    [Serializable]
    public class OrgPostUser : EntityBase
    {
        public const string PropertyName_PostId = "PostId";

        /// <summary>
        /// 获取或设置角色Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string PostId { get; set; }

        public const string PropertyName_UserId = "UserId";

        /// <summary>
        /// 获取或设置用户ID
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 获取或设置显示顺序
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.OrgPostUser;
            }
        }

    }
}