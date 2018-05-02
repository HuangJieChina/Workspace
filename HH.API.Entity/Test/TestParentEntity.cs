using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.API.Entity
{
    /// <summary>
    /// 角色用户映射表
    /// </summary>
    [Serializable]
    public class TestParentEntity : EntityBase
    {
        /// <summary>
        /// 获取或设置角色Id
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string RoleId { get; set; }

        /// <summary>
        /// 获取或设置用户ID
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 获取或设置当前用户做为本角色的服务范围，默认服务范围为本组织
        /// </summary>
        public List<string> UnitScopes { get; set; }

        /// <summary>
        /// 获取或设置显示顺序
        /// </summary>
        public int SortKey { get; set; }

        /// <summary>
        /// 子对象
        /// </summary>
        [NotMapped]
        public TestChildEntity TestChild
        {
            get; set;
        }

        [NotMapped]
        public List<TestUserEntity> TestUser
        {
            get; set;
        }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return "Test_Parent";
            }
        }

    }

    ///// <summary>
    ///// 实体属性映射
    ///// </summary>
    //[Serializable]
    //public class TestParentEntityMapper : ClassMapperBase<TestParentEntity>
    //{

    //}
}