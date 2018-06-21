using System;
using Test.Test;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ValuesTest valuesTest = new ValuesTest();
            valuesTest.Test_Method1();
            valuesTest.Test_Test3();

            Console.ReadLine();
        }
    }
}
