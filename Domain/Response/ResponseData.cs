using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    public class ResponseData
    {
        public object data { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public string code { get; set; }
        public Boolean successed { get; set; }
   
    }
}
