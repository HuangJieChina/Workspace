﻿using HH.API.Entity;
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
    public class WorkflowTemplateRepository : RepositoryBase<WorkflowTemplate>,
        IWorkflowTemplateRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowTemplateRepository()
        {

        }
    }
}