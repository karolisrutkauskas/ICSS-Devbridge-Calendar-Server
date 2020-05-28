using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models
{
    public class ErrorLog
    {
        public int LogId { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
    }
}