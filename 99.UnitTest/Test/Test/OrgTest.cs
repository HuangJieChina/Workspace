﻿using HH.API.Entity;
using HH.API.Entity.Orgainzation;
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

            OrgDepartment rootUnit = JsonConvert.DeserializeObject<OrgDepartment>(rootUnitValue);

            OrgDepartment unit1 = new OrgDepartment()
            {
                DisplayName = "测试部",
                ParentId = rootUnit.ObjectId
            };

            string result = this.HttpPost(ServerUri + "/Org/AddOrgUnit", JsonConvert.SerializeObject(unit1));
            Console.WriteLine("Org.AddOrgUnit->" + result);

            OrgDepartment unit2 = new OrgDepartment()
            {
                DisplayName = "开发部",
                ParentId = string.Empty
            };

            string result1 = this.HttpPost(ServerUri + "/Org/AddOrgUnit", JsonConvert.SerializeObject(unit2));
            Console.WriteLine("Org.AddOrgUnit->" + result1);
        }
    }
}
