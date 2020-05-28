using Dapper;
using Dapper.Contrib.Extensions;
using DevBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Dao
{
    public class ExceptionDao : IExceptionDao
    {
        public void InsertException(ErrorLog exception)
        {
            using (var db = new DbContext())
            {
                var sql = "INSERT INTO ErrorLog VALUES (@ExceptionType, @ExceptionMessage, @StackTrace, @Timestamp)";
                db.Connection.Execute(sql, new {
                    exception.ExceptionType,
                    exception.ExceptionMessage,
                    exception.StackTrace,
                    exception.Timestamp
                });
            }
        }
    }
}