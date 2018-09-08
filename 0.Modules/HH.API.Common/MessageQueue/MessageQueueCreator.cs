using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Common
{
    public class MessageQueueCreator
    {
        /// <summary>
        /// 获取一个MessageQueue
        /// </summary>
        /// <returns></returns>
        public MessageQueue<T> GetMessageQueue<T>()
        {
            return new MessageQueue<T>();
        }
    }
}
