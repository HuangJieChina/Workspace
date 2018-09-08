using System;
using System.Collections.Generic;

namespace HH.API.Common
{
    public interface IMessageQueue<T>
    {
        /// <summary>
        /// 新增一个消息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool AddMessage(T t);

        /// <summary>
        /// 获取所有消息
        /// </summary>
        /// <returns></returns>
        List<T> PeekAll();
    }
}
