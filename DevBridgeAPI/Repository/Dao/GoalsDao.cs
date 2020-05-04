using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Dao
{
    public class GoalsDao : IModelSelector, IGoalsDao
    {
        public IEnumerable<IModel> SelectAllRows()
        {
            string sql = "SELECT * FROM Goals";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Goal>(sql);
            }
        }

        public IEnumerable<Goal> SelectByUserId(int userId)
        {
            string sql = "SELECT * FROM Goals " +
                         "WHERE UserId = @userId";

            using (var db = new DbContext())
            {
                return db.Connection.Query<Goal>(sql, new { userId });
            }
        }
    }
}