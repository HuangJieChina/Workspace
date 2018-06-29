using System;
using System.Collections.Generic;
using System.Dynamic;

namespace HH.Container
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic cusDynamic = new CusDynamicObject();
            cusDynamic.Name = "HuangJie";
            cusDynamic.Code = "zhangs";
            Console.WriteLine(cusDynamic.Code);

            Console.WriteLine("Hello World!");
        }
    }

    class CusDynamicObject : DynamicObject
    {
        Dictionary<string, object> _dynamicData = new Dictionary<string, object>();
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            bool success = false;
            result = null;
            if (_dynamicData.ContainsKey(binder.Name))
            {
                result = _dynamicData[binder.Name];
                success = true;
            }
            else
                result = "Property Not Found!";

            return success;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dynamicData[binder.Name] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            dynamic method = _dynamicData[binder.Name];
            result = method((DateTime)args[0]);
            return result != null;
        }

    }
}