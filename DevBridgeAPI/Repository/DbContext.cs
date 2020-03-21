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
    public class DbContext : IDisposable
    {
        public SqlConnection Connection { get => _connection; }

        private readonly SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevBridgeDB"].ConnectionString);

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}