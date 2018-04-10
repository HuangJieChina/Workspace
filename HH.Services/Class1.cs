using DapperExtensions.Mapper;
using HH.API.Entity;
using System;

namespace HH.API.Services
{
    public class OrgUnitRepository : ClassMapper<OrgUnit>
    {
        public OrgUnitRepository()
        {
            Table(EntityConfig.Table.OrgUnit);
            Map(x => x.ObjectId).Key(KeyType.Identity);
            AutoMap();
        }
    }
}