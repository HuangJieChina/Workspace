using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.Container
{
    public class TaskAwait
    {
        public TaskAwait()
        {

        }

        public void TestAsync()
        {
            Console.WriteLine("TestAsync 1");
            // Task<string> res = this.GetTest("huangj");
            Console.WriteLine("TestAsync 2");
            TestTask();
            Console.WriteLine("TestAsync 3");
            // Console.WriteLine("TestAsync 6->" + res.Result);
        }

        public void TestTask()
        {
            Console.WriteLine("TestTask start ...");
            Task.Run(() =>
            {
                Console.WriteLine("TestTask1 sleep start ...");
                Thread.Sleep(2 * 1000);
                Console.WriteLine("TestTask1 sleep end...");
            });

            Task.Run(() =>
            {
                Console.WriteLine("TestTask2 sleep start ...");
                Thread.Sleep(2 * 1000);
                Console.WriteLine("TestTask2 sleep end...");
            });
            Console.WriteLine("TestTask End ...");
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public async Task<string> GetTest(string inputValue)
        {
            Console.WriteLine("GetTest start...");

            Console.WriteLine("Thread sleep start...");
            Thread.Sleep(2 * 1000);
            Console.WriteLine("Thread sleep end...");

            return await Task.Run<string>(() =>
            {
                Console.WriteLine("Thread1 sleep start ...");
                Thread.Sleep(2 * 1000);
                Console.WriteLine("Thread1 sleep end...");
                return "Hello," + inputValue;
            });
        }
    }
}
