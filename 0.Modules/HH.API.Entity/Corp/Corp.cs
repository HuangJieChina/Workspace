using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity
{
    public class Corp : EntityBase
    {
        /// <summary>
        /// 获取或设置企业CorpId
        /// </summary>
        public string CorpId { get; set; }

        /// <summary>
        /// 获取或设置企业秘钥
        /// </summary>
        public string Secret { get; set; }

        public override string TableName => throw new NotImplementedException();
    }
}