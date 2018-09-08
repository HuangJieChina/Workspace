using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.IAdapters
{
    public class BizMethod
    {
        public string MethodName { get; set; }

        public List<BizProperty> Paramters { get; set; }

        public List<BizProperty> Returns { get; set; }
    }

    public enum MethodType
    {
        SingleObject = 0,
        ArrayObject = 0
    }
}
