using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.API.Controllers
{
    /// <summary>
    /// 配置信息类
    /// </summary>
    public class Config
    {
        public const string API_Issuer = "Authine";
        public const string API_Audience = "API";

        private static SymmetricSecurityKey _SymmetricKey = null;
        /// <summary>
        /// 获取JWT秘钥
        /// </summary>
        public static SymmetricSecurityKey SymmetricKey
        {
            get
            {
                if (_SymmetricKey == null)
                {
                    string key = "huangj@authine.com";
                    _SymmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                }
                return _SymmetricKey;
            }
        }
    }
}
