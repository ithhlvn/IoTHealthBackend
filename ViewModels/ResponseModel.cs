using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibs.Libs;
using IOT.Libs;

namespace IOT.ViewModels
{
    public class ResponseModel
    {
        ApiCodes ApiCodes { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
