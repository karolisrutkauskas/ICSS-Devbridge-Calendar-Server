using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Selector
{
    public class TeamsSelector : IModelSelector
    {
        public IEnumerable<IModel> SelectAllRows()
        {
            string sql = "SELECT * FROM Teams";
            using (var db = new DbContext().Connection)
            {
                return db.Query<Team>(sql).ToList();
            }
        }
    }
}