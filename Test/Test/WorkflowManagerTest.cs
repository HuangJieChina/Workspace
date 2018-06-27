using HH.API.Entity;
using HH.API.Entity.BizObject;
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

        public void Test_AddBizProperty()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("schemaCode", "POTest");

            string bizSchemaValue = this.HttpGet(ServerUri + "/WorkflowManager/GetBizSchemaByCode", parameters);

            BizSchema bizSchema = JsonConvert.DeserializeObject<BizSchema>(bizSchemaValue);

            BizProperty property1 = new BizProperty()
            {
                ParentSchemaCode = bizSchema.SchemaCode,
                PropertyName = "Title",
                LogicType = LogicType.String
            };
            string result = this.HttpPost(ServerUri + "/WorkflowManager/AddBizProperty", JsonConvert.SerializeObject(property1));
            Console.WriteLine("Test_AddBizProperty1->" + result);

            BizProperty property2 = new BizProperty()
            {
                ParentSchemaCode = bizSchema.SchemaCode,
                PropertyName = "Money",
                LogicType = LogicType.Decimal
            };
            result = this.HttpPost(ServerUri + "/WorkflowManager/AddBizProperty", JsonConvert.SerializeObject(property2));
            Console.WriteLine("Test_AddBizProperty2->" + result);
        }
    }
}
