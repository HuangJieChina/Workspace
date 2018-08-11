using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IServices;
using HH.API.Entity.BizModel;

namespace HH.API.Services
{
    public class BizPackageRepository : RepositoryBase<BizPackage>,
        IBizPackageRepository
    {
        public BizPackageRepository() : base()
        {
        }

        public int Count { get; set; }


        public string SayHello(string inputValue)
        {
            return "Hello," + inputValue;
        }

        /// <summary>
        /// 根据编码获取对象
        /// </summary>
        /// <param name="packageCode"></param>
        /// <returns></returns>
        public BizPackage GetBizPackageByCode(string packageCode)
        {
            return this.GetObjectByKey(BizPackage.PropertyName_PackageCode, packageCode);
        }
    }
}