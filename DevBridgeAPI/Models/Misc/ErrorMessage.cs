using System;
using System.Web.Http.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Misc
{
    public class ErrorMessage
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}