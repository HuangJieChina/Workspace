using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HH.API.Entity;
using HH.API.Services;
using HH.API.IController;
using HH.API.IServices;
using Microsoft.AspNetCore.Authorization;
using HH.API.Entity.BizModel;

namespace HH.API.Controllers
{
    [Route("api/[controller]")]
    public class BizModelController : WorkflowController
    {
        public BizModelController(IBizPackageRepository workflowPackageRepository,
            IBizSchemaRepository bizSchemaRepository, IBizSheetRepository bizSheetRepository,
            IWorkflowTemplateRepository workflowTemplateRepository,
            IAppFunctionRepository functionNodeRepository)
            : base(workflowPackageRepository, bizSchemaRepository, bizSheetRepository,
                  workflowTemplateRepository, functionNodeRepository)
        {

        }
    }
}
