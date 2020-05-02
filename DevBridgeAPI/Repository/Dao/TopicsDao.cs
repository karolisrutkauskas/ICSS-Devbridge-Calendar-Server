using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Dao
{
    public class TopicsDao : IModelSelector, ITopicsDao
    {
        public IEnumerable<IModel> SelectAllRows()
        {
            string sql = "SELECT * FROM Topics";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Topic>(sql);
            }
        }
    }
}