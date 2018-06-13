using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Common
{
    public class MessageQueue<T> : IMessageQueue<T>
    {
        public bool AddMessage(T t)
        {
            throw new NotImplementedException();
        }

        public List<T> PeekAll()
        {
            throw new NotImplementedException();
        }
    }
}
