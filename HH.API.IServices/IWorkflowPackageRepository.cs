using HH.API.Entity;
using System;

namespace HH.API.IServices
{
    public interface IWorkflowPackageRepository : IRepositoryBase<WorkflowPackage>
    {
        string SayHello(string inputValue);
    }
}