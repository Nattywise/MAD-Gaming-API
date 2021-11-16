using System;
using System.Collections.Generic;
using System.Text;

namespace MadDataAccess.ModelCustom
{
    public class Meta
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public Meta()
        {

        }

        public Meta(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
    public class SimpleResponse
    {
        public Meta Meta { get; set; }

        public string Data { get; set; }

        public SimpleResponse()
        {
            Meta = new Meta();
        }
    }
}
