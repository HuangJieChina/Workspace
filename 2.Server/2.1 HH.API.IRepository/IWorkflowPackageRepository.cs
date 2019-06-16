using HH.API.Entity;
using HH.API.Entity.BizModel;
using System;

namespace HH.API.IRepository
{
    public interface IBizPackageRepository : IRepositoryBase<BizPackage>
    {
        string SayHello(string inputValue);

        /// <summary>
        /// 根据Code获取对象
        /// </summary>
        /// <param name="packageCode">Code</param>
        /// <returns>实体对象</returns>
        BizPackage GetBizPackageByCode(string packageCode);
    }
}