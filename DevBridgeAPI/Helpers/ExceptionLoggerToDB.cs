using DevBridgeAPI.Models;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Helpers
{
    public class ExceptionLoggerToDB : IExceptionLogger
    {
        private readonly IExceptionDao exceptionDao;

        public ExceptionLoggerToDB (IExceptionDao exceptionDao) {
            this.exceptionDao = exceptionDao;
        }

        public void LogException(Exception exception)
        {
            var exceptionToLog = new ErrorLog();
            exceptionToLog.ExceptionType = exception.GetType().Name;
            exceptionToLog.ExceptionMessage = exception.Message;
            exceptionToLog.StackTrace = exception.StackTrace;
            exceptionToLog.Timestamp = DateTime.Now;

            exceptionDao.InsertException(exceptionToLog);
        }
    }
}