using System;
using System.Web.Http.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Misc
{
    public class ErrorMessage
    {
        /// <summary>
        /// Error code that should be unique for the message type
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }
}