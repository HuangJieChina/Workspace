using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.Entity.Orgainzation;
using HH.API.IRepository;

namespace HH.API.Repository
{
    public class OrgPostUserRepository : RepositoryBase<OrgPostUser>, IOrgPostUserRepository
    {
        public OrgPostUserRepository() : base()
        {
        }
    }
}