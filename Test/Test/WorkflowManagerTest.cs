using HH.API.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Test.Test
{
    public class WorkflowManagerTest : HttpBase
    {
        public WorkflowManagerTest() { }

        public void Test_AddFolder()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ParentID", null);
            parameters.Add("functionName", "测试目录");
            parameters.Add("sortOrder", "1");
            parameters.Add("isRoot", "true");

            string result = this.HttpGet(ServerUri + "/WorkflowManager/AddWorkflowFolder", parameters);
            Console.WriteLine("WorkflowManager.AddWorkflowFolder->" + result);
        }

        public void Test_AddWorkflowPackage()
        {
            List<FunctionNode> functionNodes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FunctionNode>>(this.HttpGet(ServerUri + "/WorkflowManager/GetRootFolders", null));

            if (functionNodes != null && functionNodes.Count > 0)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("folderId", functionNodes[0].ObjectId);
                parameters.Add("packageCode", "POTest");
                parameters.Add("packageName", "采购流程");
                parameters.Add("sortOrder", "1");

                string result = this.HttpGet(ServerUri + "/WorkflowManager/AddWorkflowPackage", parameters);
                Console.WriteLine("WorkflowManager.AddWorkflowPackage->" + result);
            }
        }
    }
}
