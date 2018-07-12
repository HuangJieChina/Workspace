using DapperExtensions;
using HH.API.Entity;
using HH.API.Entity.BizModel;
using System;
using System.Collections.Generic;

namespace HH.API.IServices
{
    public interface IBizSchemaRepository : IRepositoryBase<BizSchema>
    {
        BizSchema GetBizSchemaByCode(string schemaCode);

        dynamic AddBizProperty(BizProperty property);

        BizProperty GetBizProperty(string objectId);

        bool PublishBizSchema(string schemaCode);
    }
}
