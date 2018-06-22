using HH.API.Entity;
using System;

namespace HH.API.IServices
{
    public interface IWorkflowPackageRepository : IRepositoryBase<WorkflowPackage>
    {
        string SayHello(string inputValue);

        int Count { get; set; }

        /// <summary>
        /// 根据Code获取对象
        /// </summary>
        /// <param name="packageCode">Code</param>
        /// <returns>实体对象</returns>
        WorkflowPackage GetWorkflowPackageByCode(string packageCode);
    }
}