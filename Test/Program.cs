﻿using HH.API.Entity;
using System;
using System.Collections.Generic;
using Test.Test;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //OrgTest orgTest = new OrgTest();
            //orgTest.Test_AddUnit();

            WorkflowManagerTest workflowManagerTest = new WorkflowManagerTest();
            workflowManagerTest.Test_AddFolder();
            workflowManagerTest.Test_AddWorkflowPackage();

            //ValuesTest valuesTest = new ValuesTest();
            //valuesTest.Test_Method1();
            //valuesTest.Test_Test3();

            Console.ReadLine();
        }
    }
}
