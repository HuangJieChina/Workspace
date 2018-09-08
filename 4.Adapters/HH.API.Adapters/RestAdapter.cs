using HH.API.IAdapters;
using System;
using System.Collections.Generic;

namespace HH.API.Adapters
{
    public class RestAdapter : IAdapter
    {
        public Dictionary<string, object> Properties { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<BizMethod> Methods { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<BizProperty> InvokeMethod()
        {
            throw new NotImplementedException();
        }

        public List<BizMethod> LoadMethods()
        {
            throw new NotImplementedException();
        }
    }
}
