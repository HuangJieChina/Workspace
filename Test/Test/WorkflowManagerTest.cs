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
                SchemaCode = bizSchema.SchemaCode,
                PropertyCode = "Title",
                LogicType = LogicType.String
            };
            string result = this.HttpPost(ServerUri + "/WorkflowManager/AddBizProperty", JsonConvert.SerializeObject(property1));
            Console.WriteLine("Test_AddBizProperty1->" + result);

            BizProperty property2 = new BizProperty()
            {
                SchemaCode = bizSchema.SchemaCode,
                PropertyCode = "Money",
                LogicType = LogicType.Numeric
            };
            result = this.HttpPost(ServerUri + "/WorkflowManager/AddBizProperty", JsonConvert.SerializeObject(property2));
            Console.WriteLine("Test_AddBizProperty2->" + result);
        }

        /// <summary>
        /// 测试发布
        /// </summary>
        public void Test_PublishSchema()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("schemaCode", "POTest");

            string result = this.HttpGet(ServerUri + "/WorkflowManager/PublishBizSchema", parameters);
            Console.WriteLine("Test_PublishSchema->" + result);
        }

        /// <summary>
        /// 测试新增业务对象
        /// </summary>
        public void Test_AddBizObject()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("schemaCode", "POTest");

            string bizSchemaValue = this.HttpGet(ServerUri + "/WorkflowManager/GetBizSchemaByCode", parameters);

            BizSchema bizSchema = JsonConvert.DeserializeObject<BizSchema>(bizSchemaValue);

            BizObject bizObject = new BizObject(bizSchema);

            bizObject.SetValue("Title", "这个是一个主题....");
            bizObject.SetValue("Money", 35.22);

            var postValue = JsonConvert.SerializeObject(bizObject);

            string result = this.HttpPost(ServerUri + "/BizObject/AddBizObject", postValue);
            Console.WriteLine("Test_AddBizObject->" + result);
        }
    }
}
