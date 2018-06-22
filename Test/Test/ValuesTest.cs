using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Test
{
    public class ValuesTest : HttpBase
    {
        public ValuesTest() { }

        public void Test_Method1()
        {
            Dictionary<string, string> paramters = new Dictionary<string, string>();
            paramters.Add("inputValue", "Hello");
            
            string result = this.HttpGet(ServerUri + "/values/Method2", paramters);
            Console.WriteLine("Test_Method1->" + result);
        }

        public void Test_Test3()
        {
            var data = new
            {
                inputValue = "Hello Test3"
            };

            string result = this.HttpPost(ServerUri + "/values/Test3", JsonConvert.SerializeObject(data));
            Console.WriteLine("Test_Test3->" + result);
        }
    }
}
