using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Orgainzation
{
    /// <summary>
    /// 用户组
    /// </summary>
    [Serializable]
    public class OrgGroup : OrganizationObject
    {
        /// <summary>
        /// 获取或设置是否组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.OrgGroup;
            }
        }
    }
}
