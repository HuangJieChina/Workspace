using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HH.API.Entity.Orgainzation
{
    /// <summary>
    /// 岗位信息
    /// </summary>
    [Serializable]
    public class OrgPost : OrganizationObject
    {
        /// <summary>
        /// 获取或设置是否组名称
        /// </summary>
        public string PostName { get; set; }

        /// <summary>
        /// 获取或设置角色编码
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 获取或设置当前用户管理范围的存储对象
        /// </summary>
        [JsonIgnore]
        [StringLength(4000)]
        public string UnitScopesValue
        {
            get
            {
                return JsonConvert.SerializeObject(this.UnitScopes);
            }
            set
            {
                this.UnitScopes = JsonConvert.DeserializeObject<List<string>>(value);
            }
        }

        /// <summary>
        /// 获取或设置当前用户做为本角色的服务范围，默认服务范围为本组织
        /// </summary>
        [NotMapped]
        public List<string> UnitScopes { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.OrgPost;
            }
        }
    }
}
