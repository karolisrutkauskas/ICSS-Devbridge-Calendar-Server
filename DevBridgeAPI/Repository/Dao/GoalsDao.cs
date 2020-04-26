using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Dao
{
    public class GoalsDao : IModelSelector
    {
        public IEnumerable<IModel> SelectAllRows()
        {
            string sql = "SELECT * FROM Goals";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Goal>(sql);
            }
        }
    }
}