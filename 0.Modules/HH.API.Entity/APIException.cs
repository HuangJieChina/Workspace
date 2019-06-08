using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity
{
    public class APIException : Exception
    {
        public APIException(APIResultCode resultCode, string message) : base(message)
        {
            this.APIResultCode = resultCode;
        }

        public APIResultCode APIResultCode { get; set; }

        public override string ToString()
        {
            return "APIResultCode:" + this.APIResultCode + "," + base.ToString();
        }
    }
}
