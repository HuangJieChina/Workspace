using HH.API.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Test.Test
{
    public class OrgTest : HttpBase
    {
        public OrgTest() { }

        public void Test_AddUnit()
        {
            string rootUnitValue = this.HttpGet(Path.Combine(ServerUri, "Org/GetRootUnit"), null);

            OrgUnit rootUnit = JsonConvert.DeserializeObject<OrgUnit>(rootUnitValue);

            OrgUnit unit1 = new OrgUnit()
            {
                UnitName = "测试部",
                ParentId = rootUnit.ObjectId
            };

            string result = this.HttpPost(ServerUri + "/Org/AddOrgUnit", JsonConvert.SerializeObject(unit1));
            Console.WriteLine("Org.AddOrgUnit->" + result);

            OrgUnit unit2 = new OrgUnit()
            {
                UnitName = "开发部",
                ParentId = string.Empty
            };

            string result1 = this.HttpPost(ServerUri + "/Org/AddOrgUnit", JsonConvert.SerializeObject(unit2));
            Console.WriteLine("Org.AddOrgUnit->" + result1);
        }
    }
}
