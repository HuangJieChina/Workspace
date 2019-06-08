using HH.API.Entity;
using HH.API.Entity.Orgainzation;
using HH.API.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Services.Org
{
    /// <summary>
    /// 承载所有的组织服务方法
    /// </summary>
    public class ServiceBase : IServiceBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceBase()
        {
        }

        /// <summary>
        /// 获取当前服务器的时间
        /// </summary>
        /// <returns></returns>
        public long GetDateTime()
        {
            return DateTime.Now.Ticks;
        }

        /// <summary>
        /// 数据格式校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected void DataValidator<T>(T t) where T : EntityBase
        {
            List<string> errors = new List<string>();
            bool validate = t.Validate(ref errors);
            if (!validate)
            {
                throw new APIException(APIResultCode.DataFromatError,
                    Newtonsoft.Json.JsonConvert.SerializeObject(errors));
            }
        }
    }
}