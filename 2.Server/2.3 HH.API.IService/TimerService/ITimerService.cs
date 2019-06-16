using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.IRepository.TimerService
{
    public interface ITimerService
    {
        /// <summary>
        /// 启动优先级比较高的进程
        /// </summary>
        void ElapsedHighThread();

        /// <summary>
        /// 启动优先级比较低的线程
        /// </summary>
        void ElapsedLowerThread();
    }
}