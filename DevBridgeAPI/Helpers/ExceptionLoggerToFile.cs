using DevBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Helpers
{
    public class ExceptionLoggerToFile : IExceptionLogger
    {
        public void LogException(Exception exception)
        {
            var exceptionToLog = new ErrorLog
            {
                ExceptionType = exception.GetType().Name,
                ExceptionMessage = exception.Message,
                StackTrace = exception.StackTrace,
                Timestamp = DateTime.Now
            };

            var filename = DateTime.Now.ToString();
            var path = @"C:\Logs\";
            System.IO.Directory.CreateDirectory(path);

            using (StreamWriter writer = new StreamWriter(path + filename.Replace(':', '-').Replace('/', '-') + ".txt"))
            {
                writer.WriteLine(exceptionToLog.Timestamp);
                writer.WriteLine(exceptionToLog.ExceptionType);
                writer.WriteLine(exceptionToLog.ExceptionMessage);
                writer.WriteLine(exceptionToLog.StackTrace);
                writer.WriteLine();
            }
        }
    }
}