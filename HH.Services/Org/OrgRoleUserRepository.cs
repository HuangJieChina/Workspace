﻿using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.IServices;
using HH.API.Entity.Orgainzation;

namespace HH.API.Services
{
    public class OrgPostUserRepository : RepositoryBase<OrgPostUser>, IOrgPostUserRepository
    {
        public OrgPostUserRepository(string corpId) : base(corpId)
        {
        }
    }
}