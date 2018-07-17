using DapperExtensions;
using HH.API.Entity.BizData;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.IServices
{
    public interface IBizObjectRepository
    {
        /// <summary>
        /// 根据业务模型编码和Id获取业务数据
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        BizObject GetBizObjectById(string schemaCode, string objectId);

        /// <summary>
        /// 更新业务数据对象
        /// </summary>
        /// <param name="bizObject"></param>
        /// <returns></returns>
        bool UpdateBizObject(BizObject bizObject);

        /// <summary>
        /// 移除业务数据对象
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        bool RemoveBizObject(string schemaCode, string objectId);

        /// <summary>
        /// 新增业务数据对象
        /// </summary>
        /// <param name="bizObject"></param>
        /// <returns></returns>
        dynamic AddBizObject(BizObject bizObject);

        /// <summary>
        /// 查询业务对象数据
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        List<BizObject> GetBizObjects(string schemaCode, object predicate = null, IList<ISort> sort = null);
    }
}