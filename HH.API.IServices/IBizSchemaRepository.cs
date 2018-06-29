using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.BizObject;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    public interface IBizSchemaRepository : IRepositoryBase<BizSchema>
    {
        BizSchema GetBizSchemaByCode(string schemaCode);

        dynamic AddBizProperty(BizProperty property);

        bool PublishBizSchema(string schemaCode);
    }
}
