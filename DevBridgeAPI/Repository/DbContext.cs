using Dapper;
using DevBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository
{
    public sealed class DbContext : IDisposable
    {
        public SqlConnection Connection { get; } = new SqlConnection(ConfigurationManager.AppSettings["appSettings:connectionStrings:DevBridgeDB"]);
        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}