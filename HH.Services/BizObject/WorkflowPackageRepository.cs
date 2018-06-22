using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.Entity.BizObject;
using HH.API.IServices;

namespace HH.API.Services
{
    public class WorkflowPackageRepository : RepositoryBase<WorkflowPackage>,
        IWorkflowPackageRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowPackageRepository()
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
        public WorkflowPackage GetWorkflowPackageByCode(string packageCode)
        {
            return this.GetObjectByKey(WorkflowPackage.PropertyName_PackageCode, packageCode);
        }
    }
}